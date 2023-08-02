using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetX.Common.Attributes;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace NetX.QuartzScheduling;

/// <summary>
/// Quartz调度服务
/// </summary>
[Singleton]
public class QuartzServer : IQuartzServer
{
    private IScheduler _scheduler;
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IServiceProvider _scopeServiceProvider;

    /// <summary>
    /// Quartz调度服务
    /// </summary>
    /// <param name="logger">日志对象</param>
    /// <param name="serviceProvider">注入服务提供器</param>
    public QuartzServer(ILogger<QuartzServer> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _scopeServiceProvider = serviceProvider.CreateScope().ServiceProvider;
    }

    /// <summary>
    /// 启动Quartz服务器
    /// </summary>
    public async Task Start(Func<NameValueCollection> config, CancellationToken cancellation = default)
    {
        //调度器工厂
        var factory = new StdSchedulerFactory(config.Invoke());
        //创建一个调度器
        _scheduler = await factory.GetScheduler(cancellation);
        //绑定自定义任务工厂
        _scheduler.JobFactory = new JobFactory(_serviceProvider);
        //添加任务监听器
        AddJobListener();
        //添加触发器监听器
        AddTriggerListener();
        //添加调度服务监听器
        AddSchedulerListener();
        //启动
        await _scheduler.Start(cancellation);
        _logger.LogInformation("Quartz server started");
    }

    /// <summary>
    /// 停止Quartz服务器
    /// </summary>
    public async Task Stop(CancellationToken cancellation = default)
    {
        if (null == _scheduler || _scheduler.IsShutdown)
            return;
        await _scheduler.Shutdown(true, cancellation);
        _logger.LogInformation("Quartz server stopped");
    }

    /// <summary>
    /// 重启Quartz服务器
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public async Task Restart(Func<NameValueCollection> config, CancellationToken cancellation = default)
    {
        await Stop(cancellation);
        await Start(config, cancellation);
    }

    /// <summary>
    /// 添加任务
    /// </summary>
    public Task AddJob(IJobDetail jobDetail, ITrigger trigger, CancellationToken cancellation = default)
    {
        return _scheduler.ScheduleJob(jobDetail, trigger, cancellation);
    }

    /// <summary>
    /// 暂停任务
    /// </summary>
    public Task PauseJob(JobKey jobKey, CancellationToken cancellation = default)
    {
        return _scheduler.PauseJob(jobKey, cancellation);
    }

    /// <summary>
    /// 暫停全部任務
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    public Task Clear(CancellationToken cancellation = default(CancellationToken))
    {
        return _scheduler.Clear(cancellation);
    }

    /// <summary>
    /// 恢复任务
    /// </summary>
    public Task ResumeJob(JobKey jobKey, CancellationToken cancellation = default)
    {
        return _scheduler.ResumeJob(jobKey, cancellation);
    }

    /// <summary>
    /// 删除任务
    /// </summary>
    public Task DeleteJob(JobKey jobKey, CancellationToken cancellation = default)
    {
        return _scheduler.DeleteJob(jobKey, cancellation);
    }

    /// <summary>
    /// 添加调度服务监听器
    /// </summary>
    private void AddSchedulerListener()
    {
        var schedulerListeners = _scopeServiceProvider.GetServices<ISchedulerListener>();
        if (!schedulerListeners.Any())
            return;
        foreach (var listener in schedulerListeners)
        {
            _scheduler.ListenerManager.AddSchedulerListener(listener);
        }
    }

    /// <summary>
    /// 添加任务监听器
    /// </summary>
    private void AddJobListener()
    {
        var jobListeners = _scopeServiceProvider.GetServices<IJobListener>();
        if (!jobListeners.Any())
            return;
        foreach (var listener in jobListeners)
        {
            _scheduler.ListenerManager.AddJobListener(listener);
        }
    }

    /// <summary>
    /// 添加触发器监听器
    /// </summary>
    private void AddTriggerListener()
    {
        var triggerListener = _scopeServiceProvider.GetServices<ITriggerListener>();
        if (!triggerListener.Any())
            return;
        foreach (var listener in triggerListener)
        {
            _scheduler.ListenerManager.AddTriggerListener(listener);
        }
    }
}
