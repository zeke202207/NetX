using Quartz;

namespace NetX.QuartzScheduling;

/// <summary>
/// 任务执行上下文
/// </summary>
public class JobTaskExecutionContext
{
    /// <summary>
    /// 任务编号
    /// </summary>
    public Guid JobId { get; set; }

    /// <summary>
    /// 任务执行上下文
    /// </summary>
    public IJobExecutionContext Context { get; set; }
}
