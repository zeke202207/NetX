using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MutilTenant;

/// <summary>
/// 多租户注入
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 多租户注入
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="service"></param>
    /// <param name="tenantType">租户类型，如果租户类型为单租户，<see cref="TenantBuilder{T}"/>将忽略所有注入</param>
    /// <returns></returns>
    public static TenantBuilder<T> AddTenancy<T>(this IServiceCollection service, TenantType tenantType = TenantType.Single)
        where T : Tenant
    => tenantType == TenantType.Multi ? new TenantBuilder<T>(service, tenantType) : new TenantBuilder<T>(service, tenantType).DefaultSingleTenant();

    /// <summary>
    /// 多租户注入 
    /// </summary>
    /// <param name="service"></param>
    /// <param name="tenantType">租户类型，如果租户类型为单租户，<see cref="TenantBuilder{T}"/>将忽略所有注入</param>
    /// <returns></returns>
    public static TenantBuilder<Tenant> AddTenancy(this IServiceCollection service, TenantType tenantType = TenantType.Single)
    => tenantType == TenantType.Multi ? new TenantBuilder<Tenant>(service, tenantType): new TenantBuilder<Tenant>(service, tenantType).DefaultSingleTenant();

}
