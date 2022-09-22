namespace NetX.Tenants;

/// <summary>
/// 租户访问访问
/// </summary>
internal class TenantAccessService<T>
    where T : Tenant
{

    private readonly ITenantResolutionStrategy _strategy;
    private readonly ITenantStore<T> _tenantStore;

    /// <summary>
    /// 租户访问访问实例
    /// </summary>
    /// <param name="tenantResolutionStrategy"></param>
    /// <param name="tenantStore"></param>
    public TenantAccessService(ITenantResolutionStrategy tenantResolutionStrategy, ITenantStore<T> tenantStore)
    {
        _strategy = tenantResolutionStrategy;
        _tenantStore = tenantStore;
    }

    /// <summary>
    /// 获取当前租户信息
    /// </summary>
    /// <returns></returns>
    public async Task<T?> GetTenatnAsync()
    {
        var tenantIdentifier = await _strategy.GetTenantIdentifierAsync();
        return await _tenantStore.GetTenantAsync(tenantIdentifier);
    }
}
