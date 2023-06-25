using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Netx.Ddd.Core;
using NetX.Common;
using NetX.TaskScheduling.Domain;
using NetX.TaskScheduling.Model;
using NetX.Tenants;
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
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var jobtaskCommand = scope.ServiceProvider.GetService<ICommandBus>();
                var tenantStore = scope.ServiceProvider.GetService<ITenantStore<Tenant>>();
                foreach (var tenant in await tenantStore.GetAllTenantAsync())
                {
                    var tenantOption = scope.ServiceProvider.GetRequiredService<TenantOption>();
                    if (null != tenantOption)
                        TenantContext.CurrentTenant.InitPrincipal(new NetXPrincipal(tenant), tenantOption);
                    //1. 从数据库获取所有任务
                    IQueryBus jobtaskQuery = scope.ServiceProvider.GetRequiredService<IQueryBus>();
                    var jobs = await jobtaskQuery.Send<JobTaskQueryAll, IEnumerable<JobTaskModel>>(new JobTaskQueryAll(string.Empty));
                    //2. 添加到调度器
                    ISchedule schedule = scope.ServiceProvider.GetRequiredService<ISchedule>();
                    foreach (var job in jobs)
                    {
                        //1.重组数据库任务状态：none
                        await jobtaskCommand.Send<SchedulerListenerCommand>(new SchedulerListenerCommand(job.Name, job.Group, JobTaskState.None));
                        //2.启动job
                        await schedule.AddJobAsync(job);
                    }
                }
            }
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
