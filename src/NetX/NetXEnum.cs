namespace NetX;

/// <summary>
/// 模块类型
/// </summary>
public enum ModuleType : byte
{
    /// <summary>
    /// 系统框架模块
    /// </summary>
    FrameworkModule,
    /// <summary>
    /// 业务模块
    /// </summary>
    UserModule
}

/// <summary>
/// code first 数据迁移支持的数据库类型
/// </summary>
public enum MigrationSupportDbType
{
    /// <summary>
    /// 
    /// </summary>
    MySql5
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
