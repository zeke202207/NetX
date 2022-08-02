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
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
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
        var mvcBuilder = builder.Services.AddMvc();
        foreach (var module in options.Modules)
        {
            //将module模块添加到 AssemblyLoadContext
            if (module.Value.module.ModuleType == ModuleType.UserModule)
                builder.InjectUserModules(mvcBuilder, builder.Services, builder.Environment, module.Value.option);
            //服务依赖注入
            module.Value.module.ConfigureServices(builder.Services, builder.Environment, 
                new ModuleContext() {  Configuration = builder.Configuration ,ModuleOptions = module.Value.option});
        }
        var app = builder.Build();
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        options.App = app;
        // 注册应用中间件组件
        foreach (var module in options.Modules)
        {
            module.Value.module.ConfigureApplication(app, builder.Environment,
                new ModuleContext() { Configuration = builder.Configuration, ModuleOptions = module.Value.option });
        }
        options.ActionConfigure?.Invoke(app);
        InternalApp.RootServices = app.Services;
        app.Run();
    }
}

