using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using NetX.DatabaseSetup;

namespace NetX;

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
    public static void Start(RunOption options, string urls = default)
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
        // 注册服应用务组件
        options.ActionServiceCollection?.Invoke(builder.Services);
        //注入系统、用户模块
        builder.InjectFrameworkService(builder.Environment, builder.Configuration);
        builder.InjectUserModulesService(options.Modules, builder.Environment, builder.Configuration);
        //所有模块数据库迁移配置完毕，注入数据库迁移
        builder.Services.BuildFluentMigrator();
        var app = builder.Build();
        //路由
        app.UseRouting();
        options.App = app;
        options.ActionConfigure?.Invoke(app);
        // 注册系统、应用中间件组件
        app.InjectFrameworkApplication(builder.Environment);
        app.InjectUserModulesApplication(options.Modules, builder.Environment);
        InternalApp.RootServices = app.Services;
        app.UseAuthentication();
        app.UseAuthorization();
        //配置端点
        app.UseEndpoints(endpoints => endpoints.MapControllers());
        app.Run();
    }
}

