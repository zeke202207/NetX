namespace NetX.Tenants;

/// <summary>
/// 数据库配置信息
/// </summary>
public class DatabaseInfo
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    public string DatabaseName { get; set; }

    /// <summary>
    /// 数据库主机
    /// </summary>
    public string DatabaseHost { get; set; }

    /// <summary>
    /// 数据库端口
    /// </summary>
    public int DatabasePort { get; set; }

    /// <summary>
    /// 数据库类型
    /// </summary>
    public DatabaseType DatabaseType { get; set; }

    /// <summary>
    /// 数据库登录user
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// 数据库登录密码 
    /// </summary>
    public string Password { get; set; }
}
