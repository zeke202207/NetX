using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Model;

/// <summary>
/// 任务名称与分组名称
/// </summary>
public class JobIdentRequest
{
    /// <summary>
    /// Job名称
    /// </summary>
    public string JobName { get; set; }

    /// <summary>
    /// Job分组名称
    /// </summary>
    public string GroupName { get; set; }
}
