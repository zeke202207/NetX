using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Utils.Logging
{
    /// <summary>
    /// 
    /// </summary>
    internal static class LoggingHostBuilderExtensions
    {
        public static IHostBuilder UseLogging(this IHostBuilder builder, LoggingType logType)
        {
            switch (logType)
            {
                case LoggingType.Serilog:
                    builder.UseSerilog((hostingContext, loggerConfiguration) =>
                    {
                        var buildConfig = new ConfigurationBuilder()
                                        .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config", "logging.json")).Build();
                        loggerConfiguration.ReadFrom.Configuration(buildConfig);
                        loggerConfiguration.Enrich.FromLogContext()
                        .WriteTo.Console();
                    });
                    return builder;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
