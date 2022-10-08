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

    /// <summary>
    /// tenants配置节点 
    /// </summary>
    public const string C_TENANT_CONFIG_TENANTS = "tenantconfig:tenants";

    /// <summary>
    /// 租户类型配置
    /// </summary>
    public const string C_TENANT_CONFIG_TENANTTYPE = "tenantconfig:tenanttype";

    /// <summary>
    /// 解析策略配置
    /// </summary>
    public const string C_TENANT_CONFIG_RESOLUTIONSTRATEGY = "tenantconfig:resolutionstrategy";

    /// <summary>
    /// 存储策略配置
    /// </summary>
    public const string C_TENANT_CONFIG_STORESTRATEGY = "tenantconfig:storestrategy";

    /// <summary>
    /// 数据库节点配置
    /// </summary>
    public const string C_TENANT_CONFIG_DATABASEINFO = "databaseinfo";

    /// <summary>
    /// 分布式数据库key
    /// </summary>
    public const string C_TENANT_DBKEY = "main";

    /// <summary>
    /// 分布式db sqllite文件
    /// </summary>
    public const string C_TENANT_DBFILE = "data source=main.db";

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
    Single = 0,
    /// <summary>
    /// 多租户系统
    /// </summary>
    Multi = 1
}
