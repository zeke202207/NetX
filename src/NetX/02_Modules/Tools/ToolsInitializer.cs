using IP2Region.Net.XDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetX.DatabaseSetup;
using NetX.Logging;
using NetX.Module;
using NetX.Tools.Core;
using System.Reflection;

namespace NetX.Tools;

/// <summary>
/// 管理员工具箱入口类
/// </summary>
internal class ToolsInitializer : ModuleInitializer
{
    public override Guid Key => new Guid(ToolsConstEnum.C_LOGGING_KEY);

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
        services.AddDatabaseLogging<DatabaseLoggingWriter>(context.Configuration, option =>
        {
            option.HandleWriteError = error => Console.WriteLine(error.ToString());
        });
        services.AddMonitorLogging(context.Configuration);
        //ip search
        services.AddSingleton<ISearcher, Searcher>();
    }
}
