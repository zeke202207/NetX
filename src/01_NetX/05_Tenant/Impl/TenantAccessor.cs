using Microsoft.AspNetCore.Http;

namespace NetX.Tenants;

/// <summary>
/// 租户访问器
/// </summary>
/// <typeparam name="T"></typeparam>
public class TenantAccessor<T> : ITenantAccessor<T> where T : Tenant
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// 租户访问器实例
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public TenantAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 租户信息
    /// </summary>
    public T Tenant => _httpContextAccessor.HttpContext.GetTenant<T>();
}
