namespace NetX.TaskScheduling.Model;

/// <summary>
/// Cron表达式触发器
/// </summary>
public class CronTriggerRequest : TriggerRequest
{
    /// <summary>
    /// cron表达式
    /// </summary>
    public string CronExpression { get; set; }
}
