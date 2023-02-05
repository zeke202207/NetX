using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netx.QuartzScheduling;

/// <summary>
/// Job生产工厂
/// </summary>
public class JobFactory : IJobFactory
{
    private readonly IServiceProvider _serviceProvider;

    public JobFactory(IServiceProvider serviceProvider)
    {
        this._serviceProvider= serviceProvider;
    }

    /// <summary>
    /// 触发器执行后创建的job实例
    /// </summary>
    /// <param name="bundle">触发器触发时内容包</param>
    /// <param name="scheduler">调度实例</param>
    /// <returns></returns>
    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return this._serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;
    }

    /// <summary>
    /// Allows the job factory to destroy/cleanup the job if needed.
    /// </summary>
    /// <param name="job">执行的job实例对象</param>
    public void ReturnJob(IJob job)
    {
     (job as IDisposable)?.Dispose();   
    }
}
