using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetX.Common;

namespace NetX.Tenants;

/// <summary>
/// 内存租户信息存储
/// </summary>
public class InMemoryTenantStore : ITenantStore<Tenant>
{
    private readonly TenantOption _tenantOption;

    /// <summary>
    /// 内存租户信息存储实例
    /// </summary>
    /// <param name="tenantOption"></param>
    public InMemoryTenantStore(TenantOption tenantOption)
    {
        _tenantOption = tenantOption;
    }

    /// <summary>
    /// 根据租户身份获取租户信息
    /// </summary>
    /// <param name="Identifier">租户身份</param>
    /// <returns></returns>
    public async Task<Tenant?> GetTenantAsync(string Identifier)
    {
        Tenant? tenant;
        if (_tenantOption.TenantType == TenantType.Multi)
            tenant = InMemoryStoreProvider.Instance.Tenants
            .SingleOrDefault(p => p.Identifier.Trim().ToLower().Equals(Identifier.Substring(0, Identifier.IndexOf(".")).Trim().ToLower()));
        else
            tenant = InMemoryStoreProvider.Instance.Tenants.FirstOrDefault();
        return await Task.FromResult(tenant);
    }
}
