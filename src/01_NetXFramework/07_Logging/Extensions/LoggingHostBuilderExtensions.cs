using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace NetX.Logging;

/// <summary>
/// 
/// </summary>
public static class LoggingHostBuilderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="logType"></param>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public static IHostBuilder UseLogging(this IHostBuilder builder, LoggingType logType)
    {
        switch (logType)
        {
            case LoggingType.Serilog:
                builder.UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration).Enrich.FromLogContext();
                });
                return builder;
            default:
                throw new NotSupportedException();
        }
    }
}
