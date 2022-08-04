using Microsoft.AspNetCore.Hosting;
using NetX.Module;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        // 添加自定义配置
        builder.Configuration.AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "config", "netx.json"));
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
        var app = builder.Build();
        //路由
        app.UseRouting();
        options.App = app;
        options.ActionConfigure?.Invoke(app);
        // 注册系统、应用中间件组件
        app.InjectFrameworkApplication(builder.Environment);
        app.InjectUserModulesApplication(options.Modules, builder.Environment);
        InternalApp.RootServices = app.Services;
        //配置端点
        app.UseEndpoints(endpoints => endpoints.MapControllers());
        app.Run();
    }
}

