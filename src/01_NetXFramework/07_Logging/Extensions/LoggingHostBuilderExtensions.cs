using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Configuration;
using Serilog.Events;

namespace NetX.Logging;

public static class LoggingHostBuilderExtensions
{
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
