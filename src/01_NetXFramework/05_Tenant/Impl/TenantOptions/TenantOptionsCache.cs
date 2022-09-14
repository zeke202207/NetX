using Microsoft.Extensions.Options;

namespace NetX.Tenants;

/// <summary>
/// 租户选项缓存
/// 为每一个租户维护一个专有的选项缓存
/// </summary>
public class TenantOptionsCache<TOptions, TTenant> : IOptionsMonitorCache<TOptions>
    where TOptions : class
    where TTenant : Tenant
{
    private readonly ITenantAccessor<TTenant> _tenantAccessor;
    private readonly TenantOptionsCacheDictionary<TOptions> _tenantSpecificOptionsCache =
        new TenantOptionsCacheDictionary<TOptions>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tenantAccessor"></param>
    public TenantOptionsCache(ITenantAccessor<TTenant> tenantAccessor)
    {
        _tenantAccessor = tenantAccessor;
    }

    /// <summary>
    /// 
    /// </summary>
    public void Clear()
    {
        _tenantSpecificOptionsCache.Get(_tenantAccessor.Tenant.TenantId).Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="createOptions"></param>
    /// <returns></returns>
    public TOptions GetOrAdd(string name, Func<TOptions> createOptions)
    {
        return _tenantSpecificOptionsCache.Get(_tenantAccessor.Tenant.TenantId)
            .GetOrAdd(name, createOptions);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public bool TryAdd(string name, TOptions options)
    {
        return _tenantSpecificOptionsCache.Get(_tenantAccessor.Tenant.TenantId)
            .TryAdd(name, options);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool TryRemove(string name)
    {
        return _tenantSpecificOptionsCache.Get(_tenantAccessor.Tenant.TenantId)
            .TryRemove(name);
    }
}
