﻿namespace NetX.TaskScheduling.Model;

/// <summary>
/// 任务实体
/// </summary>
public class JobTaskInfo
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
    public string JobDataMap { get; set; }

    /// <summary>
    /// 任务是否启用
    /// </summary>
    public bool Enabled { get; set; }

    /// <summary>
    /// 任务运行状态
    /// </summary>
    public int State { get; set; }

    /// <summary>
    /// 是否允许并发执行
    /// </summary>
    public bool DisAllowConcurrentExecution { get; set; }


}
