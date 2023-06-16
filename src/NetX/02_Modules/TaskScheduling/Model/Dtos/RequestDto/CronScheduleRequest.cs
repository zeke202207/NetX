namespace NetX.TaskScheduling.Model;

/// <summary>
/// 任务调度实体对象
/// </summary>
public class CronScheduleRequest
{
    /// <summary>
    /// 任务唯一标识
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 任务配置
    /// </summary>
    public JobTaskInfo Job { get; set; }

    /// <summary>
    /// 触发器配置
    /// </summary>
    public CronTriggerInfo Trigger { get; set; }
}