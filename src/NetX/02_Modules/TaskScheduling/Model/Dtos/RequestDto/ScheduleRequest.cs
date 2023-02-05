using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Model;

/// <summary>
/// 
/// </summary>
public abstract class ScheduleRequest
{
    /// <summary>
    /// 任务唯一标识
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// 任务配置
    /// </summary>
    public JobRequest Job { get; set; }

}
