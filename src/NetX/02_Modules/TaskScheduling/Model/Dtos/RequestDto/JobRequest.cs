namespace NetX.TaskScheduling.Model;

/// <summary>
/// 任务实体
/// </summary>
public class JobRequest
{
    /// <summary>
    /// 任务名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 任务分组
    /// </summary>
    public string Group { get; set; }

    /// <summary>
    /// 任务描述
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// 任务类型
    /// </summary>
    public string JobType { get; set; }

    /// <summary>
    /// 任务参数
    /// </summary>
    public Dictionary<string, string> JobDataMap { get; set; }
}
