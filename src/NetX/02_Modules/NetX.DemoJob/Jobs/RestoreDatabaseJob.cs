using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using NetX.QuartzScheduling;
using NetX.App;
using NetX.DatabaseSetup;
using NetX.TaskScheduling.Core;
using NetX.Tenants;
using NetX.Tenants.Extensions;
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
        _serviceProvider.MockTenantScope(jobTenantId, (scope, tenant) =>
        {
            //1.迁移服务
            var _migrationService = scope.ServiceProvider.GetService<IMigrationService>();
            if (null == _migrationService)
                return;
            //1.清空数据库
            _migrationService.MigrateDown(0).GetAwaiter().GetResult();
            //2.还原数据库
            _migrationService.MigrateUp(false).GetAwaiter().GetResult();
            //3.停止当前任务
            _quartzServer.Clear().GetAwaiter().GetResult();
        });
    }
}
