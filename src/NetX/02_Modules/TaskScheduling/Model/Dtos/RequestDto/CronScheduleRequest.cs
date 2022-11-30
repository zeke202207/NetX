namespace NetX.TaskScheduling.Model;

/// <summary>
/// 任务调度实体对象
/// </summary>
public class CronScheduleRequest : ScheduleRequest
{
    /// <summary>
    /// 触发器配置
    /// </summary>
    public CronTriggerRequest Trigger { get; set; }
}