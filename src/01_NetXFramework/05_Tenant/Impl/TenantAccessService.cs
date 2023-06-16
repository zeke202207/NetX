namespace NetX.Tenants;

/// <summary>
/// 租户访问访问
/// </summary>
internal class TenantAccessService<T>
    where T : Tenant
{
    private readonly ITenantStore<T> _tenantStore;

    /// <summary>
    /// 租户访问访问实例
    /// </summary>
    /// <param name="tenantStore"></param>
    public TenantAccessService( ITenantStore<T> tenantStore)
    {
        _tenantStore = tenantStore;
    }

    /// <summary>
    /// 获取当前租户信息
    /// </summary>
    /// <returns></returns>
    public async Task<T?> GetTenatnAsync()
    {
        return await _tenantStore.GetTenantAsync();
    }
}
