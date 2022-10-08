using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NetX.Tenants;

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
    => tenantType == TenantType.Multi ? new TenantBuilder<Tenant>(service, tenantType) : new TenantBuilder<Tenant>(service, tenantType).DefaultSingleTenant();

    /// <summary>
    /// 多租户注入
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="service"></param>
    /// <param name="config">
    /// 租户类型：单租户、多租户
    /// 租户解析策略
    /// 租户存储策略
    /// </param>
    /// <returns></returns>
    public static TenantBuilder<T> AddTenancy<T>(this IServiceCollection service, IConfiguration config)
        where T : Tenant
        => new TenantBuilder<T>(service, config);

    /// <summary>
    /// 多租户注入
    /// </summary>
    /// <param name="service"></param>
    /// <param name="config">
    /// 租户类型：单租户、多租户
    /// 租户解析策略
    /// 租户存储策略
    /// </param>
    /// <returns></returns>
    public static TenantBuilder<Tenant> AddTenancy(this IServiceCollection service, IConfiguration config)
        => new TenantBuilder<Tenant>(service, config);
}
