namespace NetX.Tenants;

/// <summary>
/// 租户访问器 
/// </summary>
public interface ITenantAccessor<T> where T : Tenant
{
    /// <summary>
    /// 租户信息
    /// </summary>
    T Tenant { get; }
}
