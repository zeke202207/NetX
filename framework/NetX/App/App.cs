using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NetX.Module;
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

    /// <summary>
    /// 根据用户模块id获取程序集上下文
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static ModuleAssemblyLoadContext GetModuleContext(Guid id)
    {
        if (!InternalApp.ModuleCotextKeyValuePairs.ContainsKey(id))
            return null;
        return InternalApp.ModuleCotextKeyValuePairs.FirstOrDefault(p => p.Key.Equals(id)).Value;
    }
}
