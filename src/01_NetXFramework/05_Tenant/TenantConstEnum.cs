namespace NetX.Tenants;

/// <summary>
/// 
/// </summary>
public class TenantConst
{
    #region Tenant

    /// <summary>
    /// http请求头租户key标识
    /// </summary>
    public const string C_TENANT_HTTPREQUESTHEADERKEY = "tenantid";

    /// <summary>
    /// tenantcontext上下文标识
    /// </summary>
    public const string C_TENANT_HTTPCONTEXTTENANTKEY = "zeke-tenant";

    #endregion
}

/// <summary>
/// 数据库类型
/// </summary>
public enum DatabaseType
{
    /// <summary>
    /// 
    /// </summary>
    MySql
}

/// <summary>
/// 租户类型
/// </summary>
public enum TenantType : byte
{
    /// <summary>
    /// 单租户系统
    /// </summary>
    Single,
    /// <summary>
    /// 多租户系统
    /// </summary>
    Multi
}
