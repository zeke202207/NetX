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
    public static IEnumerable<ModuleOptions> GetModuleOptions => InternalApp.UserModeulOptions;

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<ModuleInitializer> GetModuleInitializer()
    {
        List<ModuleInitializer> result = new List<ModuleInitializer>();
        foreach(var item in InternalApp.FrameworkContextKeyValuePairs)
        {
            result.Add(item.Value.initializer);
        }
        foreach(var item in InternalApp.ModuleCotextKeyValuePairs)
        {
            if (null != item.Value && null != item.Value.ModuleContext && null != item.Value.ModuleContext.Initialize)
                result.Add(item.Value.ModuleContext.Initialize);
        }
        return result;
    }
}
