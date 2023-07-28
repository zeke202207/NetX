using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetX.Module;

namespace NetX.App;

/// <summary>
/// 全局应用程序类
/// </summary>
public static class App
{
    /// <summary>
    /// 根服务
    /// </summary>
    public static IServiceProvider? RootServices => InternalApp.RootServices;

    public static IServiceCollection Services => InternalApp.Services;

    /// <summary>
    /// 获取请求上下文
    /// </summary>
    public static HttpContext? HttpContext => RootServices?.GetService<IHttpContextAccessor>()?.HttpContext;

    /// <summary>
    /// 获取配置信息
    /// </summary>
    public static IConfiguration? Configuration => InternalApp.Configuration;

    /// <summary>
    /// 构造函数
    /// </summary>
    static App()
    {

    }

    /// <summary>
    /// 获取模块配置信息
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<ModuleOptions> GetUserModuleOptions => InternalApp.UserModeulOptions;

    /// <summary>
    /// 获取全部模块信息
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<ModuleInitializer> GetModuleInitializer()
    {
        return InternalApp.ModuleContexts.Select(p => p.Value.Initialize);
    }
}
