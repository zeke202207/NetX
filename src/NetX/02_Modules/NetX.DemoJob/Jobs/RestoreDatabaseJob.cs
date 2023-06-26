using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Netx.QuartzScheduling;
using NetX.App;
using NetX.DatabaseSetup;
using NetX.TaskScheduling.Core;
using NetX.Tenants;
using Quartz.Util;
using System;
using System.Collections.Specialized;
using System.Text;

namespace NetX.DemoJob;

/// <summary>
/// 数据库定时还原job，用于演示数据库
/// </summary>
[JobTaskAttribute("1","数据库定时还原")]
public class RestoreDatabaseJob : JobTask
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IQuartzServer _quartzServer;

    /// <summary>
    /// 数据库还原job实例
    /// </summary>
    /// <param name="logger">日志对象</param>
    public RestoreDatabaseJob(IJobTaskLogger logger, IServiceProvider serviceProvider, IQuartzServer quartzServer)
        : base(logger)
    {
        _serviceProvider = serviceProvider;
        _quartzServer = quartzServer;
    }

    /// <summary>
    /// 任务入口
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task Execute(JobTaskExecutionContext context)
    {
        var jobTenantId = context.Context.JobDetail.JobDataMap["tenantid"]?.ToString();
        if (string.IsNullOrWhiteSpace(jobTenantId))
            return;
        using (IServiceScope scope = _serviceProvider.CreateScope())
        {
            var tenantStore = scope.ServiceProvider.GetService<ITenantStore<Tenant>>();
            var allTenant = await tenantStore.GetAllTenantAsync();
            var jobTenant = allTenant?.FirstOrDefault(p => p.TenantId.Equals(jobTenantId));
            if (null == jobTenant)
                return;
            //0.init the current tenant information
            var tenantOption = scope.ServiceProvider.GetRequiredService<TenantOption>();
            if (null != tenantOption)
                TenantContext.CurrentTenant.InitPrincipal(new NetXPrincipal(jobTenant), tenantOption);
            //1.迁移服务
            var _migrationService = scope.ServiceProvider.GetService<IMigrationService>();
            if (null == _migrationService)
                return;
            //1.清空数据库
            await _migrationService.MigrateDown(0);
            //2.还原数据库
            await _migrationService.MigrateUp(false);
            //3.停止当前任务
            await _quartzServer.Clear();
        }
    }
}
