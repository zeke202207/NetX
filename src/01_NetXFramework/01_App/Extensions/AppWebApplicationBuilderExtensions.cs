using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using Netx.QuartzScheduling;
using NetX.Common;
using NetX.DatabaseSetup;
using NetX.Module;
using System.Runtime.Loader;

namespace NetX.App;

/// <summary>
/// 应用程序扩展
/// </summary>
public static class AppWebApplicationBuilderExtensions
{
    /// <summary>
    /// 注入配置文件
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="options">启动配置参数</param>
    /// <returns></returns>
    public static WebApplicationBuilder AddConfiguration(
        this WebApplicationBuilder builder,
        RunOption options
        )
    {
        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AppConst.C_APP_CONFIG_FILE_NAME);
        foreach (var configFile in Directory.EnumerateFiles(configPath, AppConst.C_APP_JSON_FILE))
            builder.Configuration.AddJsonFile(configFile);
        // 添加自定义配置
        options.ActionConfigrationManager?.Invoke(builder.Configuration);
        options.Builder = builder;
        InternalApp.Configuration = builder.Configuration;
        return builder;
    }

    /// <summary>
    /// 注入系统服务
    /// </summary>
    /// <param name="webApplicationBuilder"></param>
    /// <param name="env"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static WebApplicationBuilder InjectFrameworkService(
        this WebApplicationBuilder webApplicationBuilder,
        IWebHostEnvironment env,
        IConfiguration config)
    {
        IServiceCollection services = webApplicationBuilder.Services;
        var objectInitialize = Activator.CreateInstance(typeof(ServerModuleInitializer));
        if (null == objectInitialize)
            return webApplicationBuilder;
        var Initialize = (ModuleInitializer)objectInitialize;
        AssemblyLoadContext.Default.LoadFromAssemblyName(Initialize.GetType().Assembly.GetName());
        var context = new ModuleContext()
        {
            Configuration = webApplicationBuilder.Configuration,
            Initialize = Initialize,
            ModuleOptions = InternalApp.FrameworkModuleOptions,
            ConfigApplication = (context,app, env) => ConfigApplication(context,app, env)
        };
        Initialize.ConfigureServices(services, env, context);
        InternalApp.ModuleContexts.Add(context.ModuleOptions.Id, context);
        return webApplicationBuilder;
    }

    /// <summary>
    /// 注入系统应用
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <returns></returns>
    public static WebApplication InjectFrameworkApplication(
        this WebApplication app,
        IWebHostEnvironment env)
    {
        if (!InternalApp.ModuleContexts.ContainsKey(ModuleSetupConst.C_SERVERHOST_MODULE_ID))
            return app;
        var context = InternalApp.ModuleContexts[ModuleSetupConst.C_SERVERHOST_MODULE_ID];
        context.ConfigApplication.Invoke(context, app, env);
        return app;
    }

    /// <summary>
    /// 注入用户模块
    /// </summary>
    /// <param name="webApplicationBuilder"></param>
    /// <param name="options"></param>
    /// <param name="env"></param>
    /// <param name="config"></param>
    /// <returns></returns>
    public static WebApplicationBuilder InjectUserModulesService(
        this WebApplicationBuilder webApplicationBuilder,
        Dictionary<Guid, ModuleOptions> options,
        IWebHostEnvironment env,
        IConfiguration config)
    {
        foreach (var option in options)
        {
            ApplicationPartManager apm = webApplicationBuilder.Services.AddControllers().PartManager;
            IServiceCollection services = webApplicationBuilder.Services;
            var contextProvider = new CollectibleAssemblyLoadContextProvider();
            ModuleContext context = new ModuleContext() 
            { 
                Configuration = config, 
                ModuleOptions = option.Value,
                ConfigApplication = (context, app, env) => ConfigApplication(context, app, env)
            };
            InternalApp.ModuleContexts.Add(context.ModuleOptions.Id, context);
            if (option.Value.IsSharedAssemblyContext)
            {
                var Initialize = contextProvider.LoadSharedCustomModule(option.Value, apm, services, env, context);
                //统一注入 
                if (Initialize != null)
                    services.AddServicesFromAssembly(Initialize.GetType().Assembly);
                context.Initialize = Initialize;
            }
            else
            {
                var alc = contextProvider.LoadCustomModule(option.Value, apm, services, env, context);
                //统一注入 
                alc.Assemblies.ToList().ForEach(asm => services.AddServicesFromAssembly(asm));
                context.Initialize = alc.ModuleContext.Initialize;
            }
        }
        return webApplicationBuilder;
    }

    /// <summary>
    /// 注入用户模块应用
    /// </summary>
    /// <param name="app"></param>
    /// <param name="options"></param>
    /// <param name="env"></param>
    /// <returns></returns>
    public static WebApplication InjectUserModulesApplication(
        this WebApplication app,
        Dictionary<Guid, ModuleOptions> options,
        IWebHostEnvironment env)
    {
        foreach (var option in options)
        {
            var context = InternalApp.ModuleContexts[option.Value.Id];
            context.ConfigApplication(context, app, env);
        }
        return app;
    }

    /// <summary>
    /// 系统模块与用户模块注入完毕 最终注入
    /// </summary>
    /// <param name="webApplicationBuilder"></param>
    /// <returns></returns>
    public static WebApplicationBuilder InjectServiceFinally(this WebApplicationBuilder webApplicationBuilder)
    {
        //0. Cache
        webApplicationBuilder.Services.AddCaches();
        //1. 任务调度统一注入job
        webApplicationBuilder.Services.AddQuartzScheduling(
            App.GetModuleInitializer().SelectMany(p => p.GetType().Assembly.GetTypes()).Where(p=> typeof(IJobTask).IsAssignableFrom(p)));
        return webApplicationBuilder;
    }

    /// <summary>
    /// 系统模块与用户模块注入完毕 最终注入
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder InjectApplicationFinally(this IApplicationBuilder app)
    {
        //路由
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        //配置端点
        app.UseEndpoints(endpoints => endpoints.MapControllers());
        return app;
    }

    /// <summary>
    /// 统一配置配置应用程序
    /// </summary>
    /// <param name="context"></param>
    /// <param name="app"></param>
    /// <param name="env"></param>
    private static void ConfigApplication(ModuleContext context, IApplicationBuilder app, IWebHostEnvironment env)
    {
        context.Initialize?.ConfigureApplication(app, env, context);
    }
}
