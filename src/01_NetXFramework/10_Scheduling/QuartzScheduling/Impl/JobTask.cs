using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netx.QuartzScheduling;

/// <summary>
/// 任务抽象类
/// 所有业务任务由此派生
/// 所有任务禁用并发
/// </summary>
[DisallowConcurrentExecution]
public abstract class JobTask : IJobTask
{
    private readonly IJobTaskLogger _logger;

    /// <summary>
    /// 抽象任务
    /// </summary>
    /// <param name="logger"></param>
    public JobTask(IJobTaskLogger logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Quartz任务调度方法
    /// </summary>
    /// <param name="context">job上下文</param>
    /// <returns></returns>
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var jobId = context.JobDetail.JobDataMap["id"];
            _logger.JobId = jobId == null ? Guid.Empty : Guid.Parse(jobId.ToString());
            await _logger.Info($"任务开始");
            await Execute(new JobTaskExecutionContext
            {
                JobId = _logger.JobId,
                Context = context
            });
        }
        catch (Exception ex)
        {
            await _logger.Error("任务异常：" + ex);
        }
        await _logger.Info("任务结束");
    }

    /// <summary>
    /// netx封装任务调度方法
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public abstract Task Execute(JobTaskExecutionContext context);
}
