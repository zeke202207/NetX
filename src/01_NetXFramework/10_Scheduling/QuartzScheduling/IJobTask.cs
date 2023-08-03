using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.QuartzScheduling;

/// <summary>
///  对Quartz的Job的封装
///  标识‘实现’任务的接口
/// </summary>
public interface IJobTask : IJob
{
    /// <summary>
    /// 触发器触发执行调用
    /// Called by the Quartz.IScheduler when a Quartz.ITrigger fires that is associated  with the Quartz.IJob.
    /// </summary>
    /// <param name="context">任务上下文</param>
    /// <returns></returns>
    Task Execute(JobTaskExecutionContext context);
}
