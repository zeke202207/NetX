using Microsoft.AspNetCore.Mvc.Filters;
using NetX.Common;
using NetX.DatabaseSetup;
using NetX.Tenants;
using System.Collections.Concurrent;
using System.Security.Claims;

namespace NetX.App;

/// <summary>
/// TenantContext过滤器
/// </summary>
public class TenantContextFilter : BaseFilter, IAuthorizationFilter, IAsyncAuthorizationFilter
{
    private readonly ITenantAccessor<Tenant> _accessor;
    private readonly TenantOption _tenantOption;
    private readonly MigrationService _migrationService;

    /// <summary>
    /// TenantContext资源过滤器实例
    /// </summary>
    /// <param name="accessor"></param>
    /// <param name="tenantOption"></param>
    /// <param name="migrationService"></param>
    public TenantContextFilter(ITenantAccessor<Tenant> accessor, TenantOption tenantOption, MigrationService migrationService)
    {
        _accessor = accessor;
        _tenantOption = tenantOption;
        _migrationService = migrationService;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var identity = context.HttpContext.User.Identity as ClaimsIdentity;
        if (null != _accessor.Tenant && null != identity)
        {
            TenantContext.CurrentTenant.InitPrincipal(new NetXPrincipal(identity, _accessor.Tenant), _tenantOption);
            _migrationService.SetupDatabase(TenantContext.CurrentTenant.Principal.Tenant.TenantId);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        OnAuthorization(context);
        await Task.CompletedTask;
    }
}
