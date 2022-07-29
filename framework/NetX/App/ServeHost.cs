using Microsoft.AspNetCore.Hosting;

namespace NetX;

/// <summary>
/// 服务宿主主机
/// </summary>
public static class ServeHost
{
    public static void Start(RunOption options, string urls = default)
    {
        //获取命令行参数
        var args = Environment.GetCommandLineArgs().Skip(1).ToArray();
        //初始化 webapplicationbuilder
        var builder = null == options.Options ?
            WebApplication.CreateBuilder(args) :
            WebApplication.CreateBuilder(options.Options);
        var startUrls = !string.IsNullOrWhiteSpace(urls) ?
            urls :
            builder.Configuration[nameof(urls)];
        if (!string.IsNullOrWhiteSpace(startUrls))
            builder.WebHost.UseUrls(startUrls);
        var app = builder.Build();
        app.Run();
    }
}

