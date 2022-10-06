using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetX.Cache.Core;
using NetX.Common;
using NetX.DatabaseSetup;
using NetX.InMemoryCache;
using NetX.Logging;
using System.ComponentModel;
using System.Reflection;

namespace NetX.App;

/// <summary>
/// 服务宿主主机
/// </summary>
public static class ServerHost
{
    /// <summary>
    /// 启动web服务器
    /// 默认端口 5000/5001
    /// </summary>
    /// <param name="options"></param>
    /// <param name="urls"></param>
    public static void Start(RunOption options, string urls = "")
    {
        //获取命令行参数
        var args = Environment.GetCommandLineArgs().Skip(1).ToArray();
        //初始化 webapplicationbuilder
        var builder = null == options.Options ?
            WebApplication.CreateBuilder(args) :
            WebApplication.CreateBuilder(options.Options);
        var configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config");
        foreach (var configFile in Directory.EnumerateFiles(configPath, "*.json"))
            builder.Configuration.AddJsonFile(configFile);
        InternalApp.Configuration = builder.Configuration;
        // 添加自定义配置
        //builder.Configuration.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "netx.json"));
        options.ActionConfigrationManager?.Invoke(builder.Configuration);
        options.Builder = builder;
        var startUrls = !string.IsNullOrWhiteSpace(urls) ?
            urls :
            builder.Configuration[nameof(urls)];
        if (!string.IsNullOrWhiteSpace(startUrls))
            builder.WebHost.UseUrls(startUrls);
        //注入系统、用户模块
        builder.InjectFrameworkService(builder.Environment, builder.Configuration);
        builder.InjectUserModulesService(options.Modules, builder.Environment, builder.Configuration);
        // 注册服应用务组件
        options.ActionServiceCollection?.Invoke(builder.Services, builder.Configuration);
        //所有模块数据库迁移配置完毕，注入数据库迁移
        builder.Services.BuildFluentMigrator();
        //添加日志
        builder.Host.UseLogging(LoggingType.Serilog);
        //Cache
        AddCaches(builder.Services);
        var app = builder.Build();
        //路由
        app.UseRouting();
        options.App = app;
        // 注册系统、应用中间件组件
        app.InjectFrameworkApplication(builder.Environment);
        app.InjectUserModulesApplication(options.Modules, builder.Environment);
        options.ActionConfigure?.Invoke(app);
        InternalApp.RootServices = app.Services;
        app.UseAuthentication();
        app.UseAuthorization();
        //配置端点
        app.UseEndpoints(endpoints => endpoints.MapControllers());
        ServiceLocator.Instance = app.Services;
        app.Run();
    }

    private static void AddCaches(IServiceCollection services)
    {
        var moduleInitializers = App.GetModuleInitializer();
        var moduleOptions = App.GetModuleOptions;
        List<CacheKeyDescriptor> list = new List<CacheKeyDescriptor>();
        foreach (var item in moduleInitializers)
        {
            var cacheKeysType = item.GetType().Assembly.GetTypes().Where(m => m.FullName.Contains("CacheKeys"));
            foreach (var type in cacheKeysType)
            {
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                foreach (var field in fields)
                {
                    var module = moduleOptions.FirstOrDefault(p => p.Id.Equals(item.Key));
                    var descriptor = new CacheKeyDescriptor
                    {
                        ModuleId = module.Id.ToString(),
                        ModuleName = module.Name,
                        Name = field.GetRawConstantValue()?.ToString(),
                        Desc = field.Name
                    };
                    var descAttr = field.GetCustomAttributes().FirstOrDefault(m =>
                        m.GetType().IsAssignableFrom(typeof(DescriptionAttribute)));
                    if (descAttr != null)
                        descriptor.Desc = ((DescriptionAttribute)descAttr).Description;
                    list.Add(descriptor);
                }
            }
        }
        services.AddInMemoryCache(() => list);
    }
}

