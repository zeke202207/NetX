using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NetX.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace NetX.EventBus;

/// <summary>
/// 事件总线后台主机服务
/// </summary>
internal sealed class EventBusHostService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _service;
    private readonly IEventSourceStorer _eventSourceStorer;
    private readonly IEnumerable<IEventSubscriber> _eventSubscribers;

    /// <summary>
    /// 事件处理程序集合
    /// </summary>
    private readonly HashSet<EventHandlerWrapper> _eventHandlers = new();

    /// <summary>
    /// 事件总线后台主机实例
    /// </summary>
    public EventBusHostService(
        ILogger<EventBusHostService> logger,
        IServiceProvider service,
        IEventSourceStorer eventSourceStorer
        //,
        //IEnumerable<IEventSubscriber> eventSubscribers
        )
    {
        _logger = logger;
        _service = service;
        _eventSourceStorer = eventSourceStorer;
        //_eventSubscribers = eventSubscribers;
        _eventSubscribers = BindingAllEventSubscribers();
        var bindingAttr = BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly;
        //逐条获取事件处理程序并进行包装
        foreach(var eventSubscriber in _eventSubscribers)
        {
            var eventSubscriberMethods = eventSubscriber.GetType().GetMethods(bindingAttr)
                .Where(m => m.IsDefined(typeof(EventSubscribeAttribute), false));
            //遍历所有订阅者事件
            foreach(var eventSubscriberMethod in eventSubscriberMethods)
            {
                //将方法转换为委托
                var handler = eventSubscriberMethod.CreateDelegate<Func<EventHandlerExecutingContext, Task>>(eventSubscriber);
                //同一个事件支持多个事件id
                var eventSubscribeAttributes = eventSubscriberMethod.GetCustomAttributes<EventSubscribeAttribute>(false);
                //逐条包装并添加到HashSet集合
                foreach(var eventSubscribeAttribute in eventSubscribeAttributes)
                {
                    _eventHandlers.Add(new EventHandlerWrapper(eventId: eventSubscribeAttribute.EventId)
                    {
                         Handler = handler
                    });
                }
            }
        }
    }

    /// <summary>
    /// 执行后台任务
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("EventBus Hosted Service is running.");

        // 注册后台主机服务停止监听
        stoppingToken.Register(() =>
            _logger.LogDebug($"EventBus Hosted Service is stopping."));

        // 监听服务是否取消
        while (!stoppingToken.IsCancellationRequested)
        {
            // 执行具体任务
            await BackgroundProcessing(stoppingToken);
        }

        _logger.LogCritical($"EventBus Hosted Service is stopped.");
    }

    /// <summary>
    /// 后台处理程序
    /// </summary>
    /// <param name="stoppingToken"></param>
    /// <returns></returns>
    private async Task BackgroundProcessing(CancellationToken stoppingToken)
    {
        //从事件存储器中读取一条
        var eventSource = await _eventSourceStorer.ReadAsync(stoppingToken);
        if(string.IsNullOrEmpty(eventSource?.EventId))
        {
            _logger.LogWarning("Invalid eventid");
            return;
        }
        var eventHandersThatShouldRun = _eventHandlers.Where(p => p.ShouldRun(eventSource.EventId));
        //空订阅
        if(!eventHandersThatShouldRun.Any())
        {
            _logger.LogWarning($"Subscriber with event Id <{eventSource.EventId}> was not found");
            return;
        }
        //创建一个任务工厂并保证任务都能使用当前的计划程序
        var taskFactory = new TaskFactory(TaskScheduler.Current);
        //逐条创建新线程调用
        foreach(var eventHandlerThatShouldRun in eventHandersThatShouldRun)
        {
            await taskFactory.StartNew(async () =>
            {
                //创建共享上下文数据对象
                var properties = new Dictionary<object,object>();
                //创建执行前上下文
                var eventHandlerEecutingContext = new EventHandlerExecutingContext(eventSource, properties)
                {
                    ExecutingTime = DateTime.UtcNow,
                };
                InvalidOperationException executionException = default;
                try
                {
                    await eventHandlerThatShouldRun.Handler(eventHandlerEecutingContext);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred executing {eventSource.EventId}");
                    executionException = new InvalidOperationException($"Error occurred executing {eventSource.EventId}", ex);
                }
                finally
                {

                }
            },stoppingToken);
        }
    }

    /// <summary>
    /// 绑定所有的处理事件
    /// </summary>
    /// <returns></returns>
    private IEnumerable<IEventSubscriber> BindingAllEventSubscribers()
    {
        List<IEventSubscriber> list = new List<IEventSubscriber>();
        AssemblyLoadContext.All.ToList().ForEach(assembly =>
        {
            assembly.Assemblies.ToList().ForEach(ass =>
            {
                ass.GetTypes().Where(type =>
                typeof(IEventSubscriber).IsAssignableFrom(type) &&
                !type.IsInterface &&
                !type.IsAbstract &&
                !type.IsSealed
                ).ToList()
                .ForEach(t => list.Add((IEventSubscriber)Activator.CreateInstance(t)));
            });
        });
        return list;
    }
}
