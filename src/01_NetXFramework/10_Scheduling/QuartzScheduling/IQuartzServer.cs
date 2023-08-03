using Quartz;
using System.Collections.Specialized;

namespace NetX.QuartzScheduling;

/// <summary>
/// 
/// </summary>
public interface IQuartzServer
{
    /// <summary>
    /// 启动Quartz服务器
    /// </summary>
    /// <param name="props">
    ///  Initializes a new instance of the Quartz.Impl.StdSchedulerFactory class.
    ///  Initialize the Quartz.ISchedulerFactory with the contents of the given key value collection object.
    /// </param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task Start(Func<NameValueCollection> props, CancellationToken cancellation = default);

    /// <summary>
    /// 停止Quartz服务器
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task Stop(CancellationToken cancellation = default);

    /// <summary>
    /// 重启Quartz服务器
    /// Stop() + Start()
    /// </summary>
    /// <param name="props"></param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task Restart(Func<NameValueCollection> props, CancellationToken cancellation = default);

    /// <summary>
    /// 添加任务
    /// Add the given Quartz.IJobDetail to the Scheduler, and associate the given Quartz.ITrigger with it.
    /// </summary>
    /// <param name="jobDetail">任务详情</param>
    /// <param name="trigger">任务触发器</param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task AddJob(IJobDetail jobDetail, ITrigger trigger, CancellationToken cancellation = default);

    /// <summary>
    /// 暂停任务
    /// Pause the Quartz.IJobDetail with the given key - by pausing all of its current Quartz.ITriggers.
    /// </summary>
    /// <param name="jobKey">Uniquely identifies a Quartz.IJobDetail.</param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task PauseJob(JobKey jobKey, CancellationToken cancellation = default);

    /// <summary>
    /// Clear全部任務
    /// </summary>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task Clear(CancellationToken cancellation = default);

    /// <summary>
    /// 恢复任务
    /// </summary>
    /// <param name="jobKey">Uniquely identifies a Quartz.IJobDetail.</param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task ResumeJob(JobKey jobKey, CancellationToken cancellation = default);

    /// <summary>
    /// 删除任务
    /// </summary>
    /// <param name="jobKey">Uniquely identifies a Quartz.IJobDetail.</param>
    /// <param name="cancellation"></param>
    /// <returns></returns>
    Task DeleteJob(JobKey jobKey, CancellationToken cancellation = default);
}
