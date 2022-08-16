using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MutilTenant;

/// <summary>
/// 租户中间件
/// </summary>
public class TenantMiddleware<T>
    where T : Tenant
{
    private readonly RequestDelegate _next;

    public TenantMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        if(!context.Items.ContainsKey(NetXConst.C_TENANT_HTTPCONTEXTTENANTKEY))
        {
            var tenantService = context.RequestServices.GetService(typeof(TenantAccessService<T>)) as TenantAccessService<T>;
            context.Items.Add(NetXConst.C_TENANT_HTTPCONTEXTTENANTKEY, await tenantService.GetTenatnAsync());
        }
        if (null != _next)
            await _next(context);
    }
}
