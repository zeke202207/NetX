using System.ComponentModel;

namespace NetX.RBAC;

/// <summary>
/// 缓存主键常量
/// </summary>
public static class CacheKeys
{
    /// <summary>
    /// 刷新令牌 
    /// <para>RBAC:ACCOUNT:REFRESHTOKEN:刷新令牌</para>
    /// </summary>
    [Description("刷新令牌")]
    public const string AUTH_REFRESH_TOKEN = "RBAC:ACCOUNT:REFRESHTOKEN:";

    /// <summary>
    /// 账户权限列表
    /// <para>RBAC:ACCOUNT:PERMISSIONS:用户唯一标识</para>
    /// </summary>
    [Description("账户权限列表")]
    public const string ACCOUNT_PERMISSIONS = "RBAC:ACCOUNT:APIPERMISSIONS:";
}
