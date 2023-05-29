using Microsoft.AspNetCore.Builder;
using Serilog;

namespace NetX.FriendlyLogging;

/// <summary>
/// 添加日志
/// </summary>
public static class LoggingHostBuilderExtensions
{
    /// <summary>
    /// 日志注入
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="logType">日志类型</param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static ConfigureHostBuilder UseLogging(this ConfigureHostBuilder builder, LoggingType logType = LoggingType.Serilog)
    {
        switch (logType)
        {
            case LoggingType.Serilog:
                builder.UseSerilog((context, services, configuration) => configuration
                   .ReadFrom.Configuration(context.Configuration, "Serilog")
                   .ReadFrom.Services(services));

                return builder;
            default:
                throw new NotSupportedException();
        }
    }
}
