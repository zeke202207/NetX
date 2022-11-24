using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Netx.QuartzScheduling;
using NetX.Authentication.Jwt;
using NetX.Common;
using NetX.EventBus;
using NetX.Module;
using NetX.Swagger;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;

namespace NetX.App;

/// <summary>
/// 内部框架服务模块初始化器
/// </summary>
public sealed class ServerModuleInitializer : ModuleInitializer
{
    /// <summary>
    /// 模块唯一标识
    /// </summary>
    public override Guid Key => new Guid("00000000000000000000000000000001");

    /// <summary>
    /// 模块类型
    /// </summary>
    public override ModuleType ModuleType => ModuleType.FrameworkModule;

    /// <summary>
    /// 配置服务
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="env"> Provides information about the web hosting environment an application is running in</param>
    /// <param name="context">模块上下文</param>
    public override void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context)
    {
        //1.跨域处理
        services.AddCors(options =>
        {
            /*浏览器的同源策略，就是出于安全考虑，浏览器会限制从脚本发起的跨域HTTP请求（比如异步请求GET, POST, PUT, DELETE, OPTIONS等等，
                    所以浏览器会向所请求的服务器发起两次请求，第一次是浏览器使用OPTIONS方法发起一个预检请求，第二次才是真正的异步请求，
                    第一次的预检请求获知服务器是否允许该跨域请求：如果允许，才发起第二次真实的请求；如果不允许，则拦截第二次请求。
                    Access-Control-Max-Age用来指定本次预检请求的有效期，单位为秒，，在此期间不用发出另一条预检请求。*/
            int preflight = context.Configuration != null ? int.Parse(context.Configuration.GetSection("cors:preflightmaxage").Value) : 0;
            var preflightMaxAge = preflight <= 0 ? new TimeSpan(0, 30, 0) : new TimeSpan(0, 0, preflight);
            var policyName = context.Configuration != null ? context.Configuration.GetSection("cors:policyname").Value : "zeke";
            options.AddPolicy(policyName,
                builder => builder.AllowAnyOrigin()
                    .SetPreflightMaxAge(preflightMaxAge)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Content-Disposition"));//下载文件时，文件名称会保存在headers的Content-Disposition属性里面
        });
        //2.添加swagger文档处理
        services.AddSwagger(App.GetUserModuleOptions.Select(p => (name: p.Name, version: p.Version, des: p.Description)));

        //3.添加HttpContext访问上下文
        services.AddHttpContextAccessor();

        //5.控制器和规范化结果
        services.AddControllers(o =>
        {
            o.Filters.Add<TenantContextFilter>();
        }).AddNewtonsoftJson();

        //6.添加事件总线
        services.AddEventBus();

        //7.授权 
        services.AddJwtAuth(context.Configuration);

        //8.注入attribute 标签服务
        services.AddServicesFromAssembly(
            new Assembly[] { 
                GetType().Assembly, 
                typeof(QuartzShutdownHandler).Assembly 
            });

        //9. 配置响应压缩方案
        services
            .Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = System.IO.Compression.CompressionLevel.Optimal;
            })
            .Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = System.IO.Compression.CompressionLevel.Optimal;
            })
            .AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
                {
                    "text/html; charset=utf-8",
                    "application/xhtml+xml",
                    "application/atom+xml",
                    "image/svg+xml"
                });
            });
        services.AddResponseCaching(options =>
        {
            //响应缓存是否区分大小写
            options.UseCaseSensitivePaths = false;
            //响应正文的最大可缓存大小（单位：字节）。默认64 * 1024 * 1024 （64 MB）
            options.MaximumBodySize = 64 * 1024 * 1024;
            //响应缓存中间件的大小限制（单位：字节）。 默认值为 100 * 1024 * 1024 （100 MB）。
            options.SizeLimit = 100 * 1024 * 1024;
        });
    }

    /// <summary>
    /// 配置应用程序
    /// 注释需要
    /// 非连续与<see cref="ConfigureServices(IServiceCollection, IWebHostEnvironment, ModuleContext)"/> 对应，方便阅读
    /// </summary>
    /// <param name="app"> Defines a class that provides the mechanisms to configure an application's request pipeline</param>
    /// <param name="env"> Provides information about the web hosting environment an application is running in</param>
    /// <param name="context">模块上下文</param>
    public override void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context)
    {
        //配置转发
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
        // 配置静态目录
        app.UseStaticFiles();

        //1.跨域
        var policyName = context.Configuration != null ? context.Configuration.GetSection("cors:policyname").Value : "zeke";
        app.UseCors(policyName);

        //2. 开发环境下开启 swagger
        if (((WebApplication)app).Environment.IsDevelopment())
            app.UseCustomSwagger(App.GetUserModuleOptions.Select(p => p.Name));
        // 添加压缩缓存
        app.UseResponseCaching();
        app.UseResponseCompression();
        app.UseQuartzScheduling(p => p?.Start(() => new NameValueCollection()
        {
            ["quartz.scheduler.instanceName"] = AppConst.C_SCHEDULING_QUARTZ_DEFAULTINSTANCENAME,
            ["quartz.jobStore.type"] = AppConst.C_SCHEDULING_QUARTZ_STORENAME,
        }));
        app.UseAppHandler();
    }
}
