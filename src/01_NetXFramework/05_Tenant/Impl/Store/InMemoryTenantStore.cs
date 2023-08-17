using Microsoft.Extensions.Configuration;

namespace NetX.Tenants;

/// <summary>
/// 内存租户信息存储
/// </summary>
public class InMemoryTenantStore : ITenantStore<Tenant>
{
    private readonly TenantOption _tenantOption;
    private readonly ITenantParseStrategy _strategy;

    /// <summary>
    /// 内存租户信息存储实例
    /// </summary>
    /// <param name="tenantOption"></param>
    public InMemoryTenantStore(TenantOption tenantOption, ITenantParseStrategy strategy)
    {
        _tenantOption = tenantOption;
        _strategy = strategy;
    }

    public Task<IEnumerable<Tenant>> GetAllTenantAsync()
    {
        if (_tenantOption.TenantType == TenantType.Multi)
            return Task.FromResult(InMemoryStoreProvider.Instance.Tenants.AsEnumerable());
        else
            return Task.FromResult(InMemoryStoreProvider.Instance.Tenants.Take(1).AsEnumerable());
    }

    /// <summary>
    /// 根据租户身份获取租户信息
    /// </summary>
    /// <param name="Identifier">租户身份</param>
    /// <returns></returns>
    public async Task<Tenant?> GetTenantAsync()
    {
        Tenant? tenant;
        if (_tenantOption.TenantType == TenantType.Multi)
        {
            tenant = InMemoryStoreProvider.Instance.Tenants
            .SingleOrDefault(p => _strategy.GetTenantIdentifierAsync().GetAwaiter().GetResult().Equals(p.Identifier));
        }
        else
            tenant = InMemoryStoreProvider.Instance.Tenants.FirstOrDefault();
        return await Task.FromResult(tenant);
    }
}
