using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MutilTenant;

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
