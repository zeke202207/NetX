using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetX.Logging.Monitors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Logging;

/// <summary>
/// 日志访问扩展类
/// </summary>
public static class LoggingServiceCollectionExtensions
{
    /// <summary>
    /// 添加日志监视器服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure">添加更多配置</param>
    /// <returns></returns>
    public static IServiceCollection AddMonitorLogging(this IServiceCollection services, IConfiguration? config, Action<LoggingMonitorSettings> configure = default)
    {
        // 读取配置
        var settings = config.GetSection(LoggingConst.C_LOGGING_CONFIG_MONITOR).Get<LoggingMonitorSettings>() ?? new LoggingMonitorSettings();
        // 添加外部配置
        configure?.Invoke(settings);
        // 注册日志监视器过滤器
        services.Configure<MvcOptions>(options =>
        {
            options.Filters.Add(new LoggingMonitorAttribute(settings));
        });
        return services;
    }

    /// <summary>
    /// 添加数据库日志服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configure">数据库日志记录器配置选项委托</param>
    /// <returns></returns>
    public static IServiceCollection AddDatabaseLogging<TDatabaseLoggingWriter>(this IServiceCollection services, Action<NetXLoggerOptions> configure)
        where TDatabaseLoggingWriter : class, ILoggingWriter
    {
        return services.AddLogging(builder => builder.AddDatabase<TDatabaseLoggingWriter>(configure));
    }

    /// <summary>
    /// 添加数据库日志服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuraionKey">配置文件对于的 Key</param>
    /// <param name="configure">数据库日志记录器配置选项委托</param>
    /// <returns></returns>
    public static IServiceCollection AddDatabaseLogging<TDatabaseLoggingWriter>(this IServiceCollection services, IConfiguration? config, Action<NetXLoggerOptions> configure = default)
        where TDatabaseLoggingWriter : class, ILoggingWriter
    {
        // 读取配置
        var options = config.GetSection(LoggingConst.C_LOGGING_CONFIG_OPTION).Get<NetXLoggerOptions>() ?? new NetXLoggerOptions();
        return services.AddLogging(builder => builder.AddDatabase<TDatabaseLoggingWriter>(options, configure));
    }
}
