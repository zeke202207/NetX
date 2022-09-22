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
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="context"></param>
    void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context);
}
