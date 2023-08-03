using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetX.Ddd.Core;
using NetX.Common;
using NetX.DatabaseSetup;
using NetX.TaskScheduling.Domain;
using NetX.TaskScheduling.Model;
using NetX.Tenants;
using NetX.Tenants.Extensions;
using System.Xml.Linq;

namespace NetX.TaskScheduling.Core;

/// <summary>
/// 程序启动加载数据库任务
/// </summary>
internal class LoadScheduleOnStart : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public LoadScheduleOnStart(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        try
        {
            _serviceProvider.MockTenantScope((scope, tenant) =>
            {
                var jobtaskCommand = scope.ServiceProvider.GetService<ICommandBus>();
                //1. 从数据库获取所有任务
                IQueryBus jobtaskQuery = scope.ServiceProvider.GetRequiredService<IQueryBus>();
                var jobs = jobtaskQuery.Send<JobTaskQueryAll, IEnumerable<JobTaskModel>>(new JobTaskQueryAll(string.Empty)).GetAwaiter().GetResult();
                //2. 添加到调度器
                ISchedule schedule = scope.ServiceProvider.GetRequiredService<ISchedule>();
                foreach (var job in jobs)
                {
                    //1.重组数据库任务状态：none
                    jobtaskCommand.Send<SchedulerListenerCommand>(new SchedulerListenerCommand(job.Name, job.Group, JobTaskState.None)).GetAwaiter().GetResult();
                    //2.启动job
                    schedule.AddJobAsync(job).GetAwaiter().GetResult();
                }
            });
        }
        catch (Exception ex)
        {
            //throw ex;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task StopAsync(CancellationToken cancellationToken)
    {

    }
}
