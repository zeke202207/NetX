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
    public static IServiceProvider RootServices => InternalApp.RootServices;

    /// <summary>
    /// 获取请求上下文
    /// </summary>
    public static HttpContext HttpContext => RootServices?.GetService<IHttpContextAccessor>()?.HttpContext;

    /// <summary>
    /// 获取配置信息
    /// </summary>
    public static IConfiguration Configuration => InternalApp.Configuration;

    /// <summary>
    /// 构造函数
    /// </summary>
    static App()
    {

    }
}
