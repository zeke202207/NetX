using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NetX.Module;

/// <summary>
/// 服务模块初始化器接口
/// </summary>
public interface IServiceModule : IModule
{
    /// <summary>
    /// 注入服务
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="env">Provides information about the web hosting environment an application is running in</param>
    /// <param name="context">模块上下文</param>
    void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context);
}
