namespace NetX.TaskScheduling.Model;

/// <summary>
/// 触发器
/// </summary>
public abstract class TriggerRequest
{
    /// <summary>
    /// 触发器名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 触发器描述
    /// </summary>
    public string Description { get; set; }
}
