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
    /// <param name="services"></param>
    /// <param name="env">环境变量</param>
    /// <param name="context"></param>
    void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context);
}
