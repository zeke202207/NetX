using Microsoft.Extensions.Logging;
using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.QuartzScheduling;

/// <summary>
/// job日志记录管理
/// </summary>
[Transient]
public class JobTaskLogger : IJobTaskLogger
{
    private readonly ILogger _logger;

    public JobTaskLogger(ILogger<JobTaskLogger> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Job唯一标识
    /// </summary>
    public Guid JobId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task Info(string msg)
    {
        _logger.LogInformation($"任务编号:{JobId}, {msg}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task Debug(string msg)
    {
        _logger.LogDebug($"任务编号:{JobId}, {msg}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="msg"></param>
    /// <returns></returns>
    public Task Error(string msg)
    {
        _logger.LogError($"任务编号:{JobId}, {msg}");
        return Task.CompletedTask;
    }
}
