﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NetX.Logging;

/// <summary>
/// 日志构建起扩展类
/// </summary>
public static class LoggingBuilderExtensions
{
    /// <summary>
    /// 添加数据库日志记录器
    /// </summary>
    /// <typeparam name="TLoggingWriter">实现自 <see cref="ILoggingWriter"/></typeparam>
    /// <param name="builder">日志构建器</param>
    /// <param name="configure">数据库日志记录器配置选项委托</param>
    /// <returns><see cref="ILoggingBuilder"/></returns>
    public static ILoggingBuilder AddDatabase<TLoggingWriter>(this ILoggingBuilder builder, 
        Action<NetXLoggerOptions> configure)
        where TLoggingWriter : class, ILoggingWriter
    {
        // 注册数据库日志写入器
        builder.Services.TryAddTransient<TLoggingWriter, TLoggingWriter>();
        var options = new NetXLoggerOptions();
        configure?.Invoke(options);
        // 数据库日志记录器提供程序
        return new NetXLoggerProvider(options).AddProvider<TLoggingWriter>(builder);
    }

    /// <summary>
    /// 添加数据库日志记录器
    /// </summary>
    /// <typeparam name="TLoggingWriter"></typeparam>
    /// <param name="builder"></param>
    /// <param name="options"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static ILoggingBuilder AddDatabase<TLoggingWriter>(this ILoggingBuilder builder,
        NetXLoggerOptions options = default, 
        Action<NetXLoggerOptions> configure = default)
        where TLoggingWriter : class, ILoggingWriter
    {
        // 注册数据库日志写入器
        builder.Services.TryAddTransient<TLoggingWriter, TLoggingWriter>();
        configure?.Invoke(options);
        // 数据库日志记录器提供程序
        return new NetXLoggerProvider(options).AddProvider<TLoggingWriter>(builder);
    }

    /// <summary>
    /// 注入日志提供器
    /// </summary>
    /// <typeparam name="TLoggingWriter"></typeparam>
    /// <param name="netXLoggerProvider"></param>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static ILoggingBuilder AddProvider<TLoggingWriter>(this NetXLoggerProvider netXLoggerProvider, ILoggingBuilder builder)
        where TLoggingWriter : class, ILoggingWriter
    {
        if (netXLoggerProvider == default)
            return builder;
        // 注册数据库日志记录器提供器
        builder.Services.AddTransient<ILoggerProvider, NetXLoggerProvider>((serviceProvider) =>
        {
            // 解决数据库写入器中循环引用数据库仓储问题
            if (netXLoggerProvider._serviceScope == null)
                netXLoggerProvider.SetServiceProvider(serviceProvider, typeof(TLoggingWriter));
            return netXLoggerProvider;
        });
        return builder;
    }
}
