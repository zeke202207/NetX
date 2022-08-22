namespace NetX.Tenants;

/// <summary>
/// 组合解析策略
/// </summary>
public interface ITenantResolutionStrategy
{
    /// <summary>
    /// 解析获取租户身份
    /// </summary>
    /// <returns></returns>
    Task<string> GetTenantIdentifierAsync();
}
