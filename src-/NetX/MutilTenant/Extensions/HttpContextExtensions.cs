using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MutilTenant;

/// <summary>
/// 扩展http上下文，使multi tenancy 使用起来更方便 
/// </summary>
internal static class HttpContextExtensions
{
    /// <summary>
    /// 获取当前租户信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="context"></param>
    /// <returns></returns>
    internal static T GetTenant<T>(this HttpContext context)
        where T :Tenant
    {
        if (!context.Items.ContainsKey(NetXConst.C_TENANT_HTTPCONTEXTTENANTKEY))
            return null;
        return context.Items[NetXConst.C_TENANT_HTTPCONTEXTTENANTKEY] as T;
    }

    /// <summary>
    /// 获取当前租户信息
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    internal static Tenant GetTenant(this HttpContext context)
    {
        return context.GetTenant<Tenant>();
    }
}
