using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetX.EventBus;

/// <summary>
/// 事件总线配置选项构建器
/// </summary>
public sealed class EventBusOptionsBuilder
{
    /// <summary>
    /// 事件订阅者类型集合
    /// </summary>
    private readonly List<Type> _eventSubscribers = new();

    /// <summary>
    /// 事件存储器实现工厂
    /// </summary>
    private Func<IServiceProvider, IEventSourceStorer> _eventSourceStorerImplementationFactory;

    /// <summary>
    /// 默认内置事件源存储器内存通道容量
    /// </summary>
    /// <remarks>超过 n 条待处理消息，第 n+1 条将进入等待，默认为 3000</remarks>
    public int ChannelCapacity { get; set; } = 3000;

    /// <summary>
    /// 注册事件订阅者
    /// </summary>
    /// <typeparam name="TEventSubscriber">实现自 <see cref="IEventSubscriber"/></typeparam>
    /// <returns><see cref="EventBusOptionsBuilder"/> 实例</returns>
    public EventBusOptionsBuilder AddSubscriber<TEventSubscriber>()
        where TEventSubscriber : class, IEventSubscriber
    {
        _eventSubscribers.Add(typeof(TEventSubscriber));
        return this;
    }

    /// <summary>
    /// 批量注册事件订阅者
    /// </summary>
    /// <param name="assemblies">程序集</param>
    /// <returns><see cref="EventBusOptionsBuilder"/> 实例</returns>
    public EventBusOptionsBuilder AddSubscribers(params Assembly[] assemblies)
    {
        if (assemblies == null || assemblies.Length == 0)
        {
            throw new InvalidOperationException("The assemblies can be not null or empty.");
        }

        // 获取所有导出类型（非接口，非抽象类且实现 IEventSubscriber）接口
        var subscribers = assemblies.SelectMany(ass =>
              ass.GetExportedTypes()
                 .Where(t => t.IsPublic && t.IsClass && !t.IsInterface && !t.IsAbstract && typeof(IEventSubscriber).IsAssignableFrom(t)));

        foreach (var subscriber in subscribers)
        {
            _eventSubscribers.Add(subscriber);
        }

        return this;
    }

    /// <summary>
    /// 替换事件源存储器
    /// </summary>
    /// <param name="implementationFactory">自定义事件源存储器工厂</param>
    /// <returns><see cref="EventBusOptionsBuilder"/> 实例</returns>
    public EventBusOptionsBuilder ReplaceStorer(Func<IServiceProvider, IEventSourceStorer> implementationFactory)
    {
        _eventSourceStorerImplementationFactory = implementationFactory;
        return this;
    }

    /// <summary>
    /// 构建事件总线配置选项
    /// </summary>
    /// <param name="services">服务集合对象</param>
    internal void Build(IServiceCollection services)
    {
        // 注册事件订阅者
        foreach (var eventSubscriber in _eventSubscribers)
        {
            services.AddSingleton(typeof(IEventSubscriber), eventSubscriber);
        }
        // 替换事件存储器
        if (_eventSourceStorerImplementationFactory != default)
            services.Replace(ServiceDescriptor.Singleton(_eventSourceStorerImplementationFactory));
    }
}
