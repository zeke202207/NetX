namespace NetX.Tenants;

/// <summary>
/// 租户信息
/// </summary>
public class Tenant
{
    /// <summary>
    /// 租户Id
    /// </summary>
    public string TenantId { get; set; } = string.Empty;

    /// <summary>
    /// 租户身份标识
    /// 在解析策略中使用此标识符进行解析
    /// </summary>
    public string Identifier { get; set; } = string.Empty;

    /// <summary>
    /// The Tenant Items
    /// </summary>
    public Dictionary<string, object> Items { get; set; } = new Dictionary<string, object>();
}
