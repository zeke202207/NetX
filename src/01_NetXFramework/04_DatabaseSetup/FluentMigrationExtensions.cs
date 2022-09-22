using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NetX.DatabaseSetup;

/// <summary>
/// 数据库迁移扩展方法
/// </summary>
public static class FluentMigrationExtensions
{
    internal readonly static List<Assembly> list = new List<Assembly>() { Assembly.GetExecutingAssembly() };

    /// <summary>
    /// 添加迁移程序集
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IServiceCollection AddMigratorAssembly(this IServiceCollection services, Assembly[] assemblies)
    {
        if (null == assemblies)
            return services;
        list.AddRange(assemblies);
        return services;
    }

    /// <summary>
    ///  数据迁移服务注入
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection BuildFluentMigrator(this IServiceCollection services)
    {
        if(!list.Any())
            return services;
        return services.BuildFluentMigrator(list.ToArray(), MigrationSupportDbType.MySql5);
    }

    /// <summary>
    /// 数据迁移服务注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <param name="supportDbType"></param>
    /// <returns></returns>
    internal static IServiceCollection BuildFluentMigrator(this IServiceCollection services, Assembly[] assemblies, MigrationSupportDbType supportDbType)
    {
        var sc = new ServiceCollection();
        sc.AddFluentMigratorCore()
            .ConfigureRunner(rb => rb
            .AddDatabase(supportDbType)
            .ScanIn(assemblies)
            .For
            .Migrations()
            .For
            .EmbeddedResources())
            .AddLogging(lb => lb.AddFluentMigratorConsole());
        services.AddSingleton(sp =>
        {
            return new MigrationService(sc, supportDbType);
        });
        return services;
    }

    /// <summary>
    /// 添加支持的数据库
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="supportDbType"></param>
    /// <returns></returns>
    private static IMigrationRunnerBuilder AddDatabase(this IMigrationRunnerBuilder builder , MigrationSupportDbType supportDbType)
    {
        switch(supportDbType)
        {
            case MigrationSupportDbType.MySql5:
            default:
                builder.AddMySql5();
                break;
        }
        return builder;
    }
}
