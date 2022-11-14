using FreeSql;
using Microsoft.AspNetCore.Http;

namespace NetX.Tenants;

/// <summary>
/// 租户中间件
/// </summary>
public class TenantMiddleware<T>
    where T : Tenant
{
    private readonly RequestDelegate _next;
    private readonly FreeSqlCloud<string>? _freeSql;
    private readonly ITenantAccessor<Tenant> _tenantAccessor;
    private readonly TenantOption _tenantOption;

    /// <summary>
    /// 租户中间件实例
    /// </summary>
    /// <param name="next"></param>
    /// <param name="freeSql"></param>
    /// <param name="tenantAccessor"></param>
    /// <param name="tenantOption"></param>
    public TenantMiddleware(RequestDelegate next, IFreeSql freeSql, ITenantAccessor<Tenant> tenantAccessor, TenantOption tenantOption)
    {
        _next = next;
        _freeSql = freeSql as FreeSqlCloud<string>;
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
        await TenancyDatabase(context);
    }

    /// <summary>
    /// 使用租户数据库
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private async Task TenancyDatabase(HttpContext context)
    {
        try
        {
            if (null != _tenantAccessor && null != _tenantOption)
                TenantContext.CurrentTenant.InitPrincipal(new NetXPrincipal(_tenantAccessor.Tenant), _tenantOption);
            if (null != TenantContext.CurrentTenant.Principal && null != TenantContext.CurrentTenant.Principal.Tenant)
            {
                _freeSql?.Register(TenantContext.CurrentTenant.Principal.Tenant.TenantId, () =>
                {
                    if (null == TenantContext.CurrentTenant.DatabaseInfo)
                        return null;
                    var db = new FreeSqlBuilder().UseConnectionString(
                        TenantContext.CurrentTenant.DatabaseInfo.DatabaseType.ToDatabaseType(),
                        TenantContext.CurrentTenant.ConnectionStr)
                    .Build();
                    //db.Aop.CommandAfter += ...
                    return db;
                });
                // 切换租户
                _freeSql?.Change(TenantContext.CurrentTenant.Principal.Tenant.TenantId);
            }
            await _next(context);
        }
        finally
        {
            // 切换回 main 库
            _freeSql?.Change(TenantConst.C_TENANT_DBKEY);
        }
    }
}
