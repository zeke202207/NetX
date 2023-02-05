using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace NetX.Module;

/// <summary>
/// application模块初始化器接口
/// </summary>
public interface IApplicationModule : IModule
{
    /// <summary>
    /// 配置中间件
    /// </summary>
    /// <param name="app"> Defines a class that provides the mechanisms to configure an application's request pipeline</param>
    /// <param name="env">Provides information about the web hosting environment an application is running in</param>
    /// <param name="context">模块上下文</param>
    void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context);
}
