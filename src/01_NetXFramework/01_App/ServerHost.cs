using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetX.Cache.Core;
using NetX.Common;
using NetX.DatabaseSetup;
using NetX.InMemoryCache;
using NetX.Logging;
using NetX.Module;
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
        builder.AddConfiguration(options);
        var startUrls = !string.IsNullOrWhiteSpace(urls) ?
            urls :
            builder.Configuration[nameof(urls)];
        if (!string.IsNullOrWhiteSpace(startUrls))
            builder.WebHost.UseUrls(startUrls);
        //注入系统服务
        builder.InjectFrameworkService(builder.Environment, builder.Configuration);
        //注入配置服务
        options.ActionServiceCollection?.Invoke(builder.Services, builder.Configuration);
        //注入用户模块服务
        builder.InjectUserModulesService(options.Modules, builder.Environment, builder.Configuration);
        builder.InjectServiceFinally();
        var app = builder.Build();
        //注册系统中间件组件
        app.InjectFrameworkApplication(builder.Environment);
        //注册配置中间件组件
        options.ActionConfigure?.Invoke(app);
        //注册应用中间件组件
        app.InjectUserModulesApplication(options.Modules, builder.Environment);
        app.InjectApplicationFinally();
        options.App = app;
        InternalApp.RootServices = app.Services;
        ServiceLocator.Instance = app.Services;
        app.Run();
    }
}

