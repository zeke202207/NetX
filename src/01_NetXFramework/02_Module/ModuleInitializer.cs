using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NetX.Module;

/// <summary>
/// Serve 组件应用服务组件
/// </summary>
public abstract class ModuleInitializer : IServiceModule, IApplicationModule
{
    /// <summary>
    /// 模块唯一标识
    /// </summary>
    public abstract Guid Key { get; }

    /// <summary>
    /// 模块类型
    /// </summary>
    public abstract ModuleType ModuleType { get; }

    /// <summary>
    /// This method gets called by the runtime.
    /// Use this method to add services to the container.
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="env">Provides information about the web hosting environment an application is running in</param>
    /// <param name="context">模块上下文</param>
    public abstract void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context);

    /// <summary>
    /// This method gets called by the runtime. 
    /// Use this method to configure the HTTP request pipeline.
    /// </summary>
    /// <param name="app"> Defines a class that provides the mechanisms to configure an application's request pipeline</param>
    /// <param name="env">Provides information about the web hosting environment an application is running in</param>
    /// <param name="context">模块上下文</param>
    public abstract void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context);
}
