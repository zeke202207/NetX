using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetX.App.Extensions;
using NetX.Authentication.Jwt;
using NetX.EventBus;
using NetX.Module;
using Serilog;

namespace NetX.App
{
    public sealed class ServerModuleInitializer : ModuleInitializer
    {
        public override Guid Key => new Guid("00000000000000000000000000000001");
        public override ModuleType ModuleType => ModuleType.FrameworkModule;

        /// <summary>
        /// 配置服务
        /// Demo宏定义下的内容为使用示例，具体是否启用，清根据业务自定决定
        /// 配置方法请参考<see cref="NetXTestWebApi.Program"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        /// <param name="context"></param>
        public override void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context)
        {
            //1.跨域处理
            services.AddCors(options =>
            {
                /*浏览器的同源策略，就是出于安全考虑，浏览器会限制从脚本发起的跨域HTTP请求（比如异步请求GET, POST, PUT, DELETE, OPTIONS等等，
                        所以浏览器会向所请求的服务器发起两次请求，第一次是浏览器使用OPTIONS方法发起一个预检请求，第二次才是真正的异步请求，
                        第一次的预检请求获知服务器是否允许该跨域请求：如果允许，才发起第二次真实的请求；如果不允许，则拦截第二次请求。
                        Access-Control-Max-Age用来指定本次预检请求的有效期，单位为秒，，在此期间不用发出另一条预检请求。*/
                int preflight = int.Parse(context.Configuration.GetSection("cors:preflightmaxage").Value);
                var preflightMaxAge = preflight <= 0 ? new TimeSpan(0, 30, 0) : new TimeSpan(0, 0, preflight);
                options.AddPolicy(context.Configuration.GetSection("cors:policyname").Value,
                    builder => builder.AllowAnyOrigin()
                        .SetPreflightMaxAge(preflightMaxAge)
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithExposedHeaders("Content-Disposition"));//下载文件时，文件名称会保存在headers的Content-Disposition属性里面
            });
            //2.添加swagger文档处理
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            //3.添加HttpContext访问上下文
            services.AddHttpContextAccessor();
            //4.启动log 

            //5.控制器和规范化结果
            services.AddControllers(o =>
            {
                o.Filters.Add<TenantContextFilter>();
            });

            //6.添加事件总线
            services.AddEventBus();

            //7.授权 
            services.AddJwtAuth(context.Configuration);

            //注入系统logo
            services.AddSingleton<IAppStartHandler, DefaultStartLogoHandler>();
        }

        /// <summary>
        /// 配置应用程序
        /// 注释需要
        /// 非连续与<see cref="ConfigureServices(IServiceCollection, IWebHostEnvironment, ModuleContext)"/> 对应，方便阅读
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="context"></param>
        public override void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context)
        {
            // 配置静态
            app.UseStaticFiles();

            //1.跨域
            app.UseCors(context.Configuration.GetSection("cors:policyname").Value);

            //2. swagger
            // Configure the HTTP request pipeline.
            if (((WebApplication)app).Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            // 添加压缩缓存
            app.UseResponseCaching();

            app.UseStartupHandler();
            app.UseShutdownHandler();
        }
    }
}
