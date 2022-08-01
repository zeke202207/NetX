using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX;

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
    /// 构造函数
    /// </summary>
    static App()
    {
       
    }
}
