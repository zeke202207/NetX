using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetX.Common;
using NetX.DiagnosticLog;
using Quartz.Util;

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
        builder.Host.UseLogging();
        builder.AddConfiguration(options);
        var startUrls = urls.IsNullOrWhiteSpace() ?
            builder.Configuration[nameof(urls)] :
            urls;
        if (!string.IsNullOrWhiteSpace(startUrls))
            builder.WebHost.UseUrls(startUrls);
        //系统logo
        builder.Configuration.ShowNetxLogo();
        //注入系统服务
        builder.InjectFrameworkService(builder.Environment, builder.Configuration);
        //注入配置服务
        options.ActionServiceCollection?.Invoke(builder.Services, builder.Configuration);
        //注入用户模块服务
        builder.InjectUserModulesService(options.Modules, builder.Environment, builder.Configuration);
        builder.InjectServiceFinally();
        builder.Services.AddSingleton(App.GetModuleInitializer());
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
        InternalApp.Services = builder.Services;
        ServiceLocator.Instance = app.Services;
        app.Run();
    }

    /// <summary>
    /// 在线生成logo图标
    /// http://patorjk.com/software/taag/#p=display&f=Blocks&t=netx%20
    /// </summary>
    /// <returns></returns>
    private static void ShowNetxLogo(this ConfigurationManager config)
    {
        if (Console.WindowWidth <= 0 || Console.WindowHeight <= 0)
            return;
        var fontcolor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.WriteLine
        (@$"
                    .-----------------. .----------------.  .----------------.  .----------------.   
                    | .--------------. || .--------------. || .--------------. || .--------------. |  
                    | | ____  _____  | || |  _________   | || |  _________   | || |  ____  ____  | |    
                    | ||_   \|_   _| | || | |_   ___  |  | || | |  _   _  |  | || | |_  _||_  _| | |    
                    | |  |   \ | |   | || |   | |_  \_|  | || | |_/ | | \_|  | || |   \ \  / /   | |
                    | |  | |\ \| |   | || |   |  _|  _   | || |     | |      | || |    > `' <    | |    
                    | | _| |_\   |_  | || |  _| |___/ |  | || |    _| |_     | || |  _/ /'`\ \_  | |
                    | ||_____|\____| | || | |_________|  | || |   |_____|    | || | |____||____| | |    
                    | |              | || |              | || |              | || |              | |    
                    | '--------------' || '--------------' || '--------------' || '--------------' |  
                    '----------------'  '----------------'  '----------------'  '----------------'   
        ");
        var c = Console.GetCursorPosition();
        string output = $"版本：{config.GetValue<string>("netxinfo:version")}";
        Console.SetCursorPosition((Console.WindowWidth - output.Length) / 2, c.Top);
        Console.WriteLine(output);
        c = Console.GetCursorPosition();
        output = $"{config.GetValue<string>("netxinfo:github")}";
        Console.SetCursorPosition((Console.WindowWidth - output.Length) / 2, c.Top);
        Console.WriteLine(output);
        c = Console.GetCursorPosition();
        Console.SetCursorPosition(0, c.Top);
        Console.WriteLine();
        Console.ForegroundColor = fontcolor;
    }
}

