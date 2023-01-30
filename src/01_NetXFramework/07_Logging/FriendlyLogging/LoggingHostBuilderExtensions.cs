using Microsoft.AspNetCore.Builder;
using Serilog;

namespace NetX.FriendlyLogging;

public static class LoggingHostBuilderExtensions
{
    public static ConfigureHostBuilder UseLogging(this ConfigureHostBuilder builder, LoggingType logType = LoggingType.Serilog)
    {
        switch (logType)
        {
            case LoggingType.Serilog:
                string OUTPUT_TEMPLATE = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} <{ThreadId}> [{Level:u3}] {Message:lj}{NewLine}{Exception}";
                Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .Enrich.WithThreadId()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: OUTPUT_TEMPLATE)
                .WriteTo.File($"logs/netx-.log"
                    , rollingInterval: RollingInterval.Day
                    , outputTemplate: OUTPUT_TEMPLATE)
                .CreateLogger();
                builder.UseSerilog(Log.Logger, dispose: true);
                return builder;
            default:
                throw new NotSupportedException();
        }
    }
}

public enum LoggingType
{
    Serilog
}