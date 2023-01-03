using Microsoft.AspNetCore.Http;

namespace NetX.Tenants;

/// <summary>
/// 租户中间件
/// </summary>
public class TenantMiddleware<T>
    where T : Tenant
{
    private readonly RequestDelegate _next;
    private readonly ITenantAccessor<Tenant> _tenantAccessor;
    private readonly TenantOption _tenantOption;

    /// <summary>
    /// 租户中间件实例
    /// </summary>
    /// <param name="next"></param>
    /// <param name="tenantAccessor"></param>
    /// <param name="tenantOption"></param>
    public TenantMiddleware(RequestDelegate next, ITenantAccessor<Tenant> tenantAccessor, TenantOption tenantOption)
    {
        _next = next;
        _tenantAccessor = tenantAccessor;
        _tenantOption = tenantOption;
    }

    /// <summary>
    /// 中间件执行方法
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task Invoke(HttpContext context)
    {
        if (!context.Items.ContainsKey(TenantConst.C_TENANT_HTTPCONTEXTTENANTKEY))
        {
            var tenantService = context.RequestServices.GetService(typeof(TenantAccessService<T>)) as TenantAccessService<T>;
            if (null != tenantService)
                context.Items.Add(TenantConst.C_TENANT_HTTPCONTEXTTENANTKEY, await tenantService.GetTenatnAsync());
        }
        if (null != _tenantAccessor && null != _tenantOption)
            TenantContext.CurrentTenant.InitPrincipal(new NetXPrincipal(_tenantAccessor.Tenant), _tenantOption);
        await _next(context);
    }
}
