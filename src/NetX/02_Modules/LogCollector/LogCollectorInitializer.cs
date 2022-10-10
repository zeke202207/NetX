using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetX.DatabaseSetup;
using NetX.LogCollector.Core;
using NetX.Logging;
using NetX.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetX.LogCollector;

/// <summary>
/// 日志采集器入口类
/// </summary>
internal class LogCollectorInitializer : ModuleInitializer
{
    public override Guid Key => new Guid(LoggingConstEnum.C_LOGGING_KEY);

    public override ModuleType ModuleType => ModuleType.UserModule;

    public override void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context)
    {

    }

    public override void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context)
    {
        //注入mapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        //code first
        services.AddMigratorAssembly(new Assembly[] { Assembly.GetExecutingAssembly() });
        //add log
        services.AddDatabaseLogging<DatabaseLoggingWriter>(context.Configuration);
        services.AddMonitorLogging(context.Configuration);
    }
}
