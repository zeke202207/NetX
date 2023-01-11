namespace NetX.RBAC;

internal class RBACConst
{
    /// <summary>
    /// rbac唯一标识
    /// </summary>
    public const string C_RBAC_KEY = "10000000000000000000000000000001";

    /// <summary>
    /// 树跟节点编号
    /// </summary>
    public const string C_ROOT_ID = "00000000000000000000000000000000";

    /// <summary>
    /// swagger分组名称
    /// </summary>
    public const string C_RBAC_GROUPNAME = "rbac";

    /// <summary>
    /// 注册账号默认密码
    /// </summary>
    public const string C_RBAC_DEFAULT_PASSWORD = "netx";

    /// <summary>
    /// 事件总线事件唯一标识
    /// </summary>
    public const string C_RBAC_EVENT_KEY = "rbac_permission_cache_key";
}

/// <summary>
/// 启用状态
/// </summary>
public enum Status
{
    /// <summary>
    /// 禁用
    /// </summary>
    Disabled = 0,
    /// <summary>
    /// 启用
    /// </summary>
    Enable = 1
}

/// <summary>
/// 外链枚举
/// </summary>
public enum Ext
{
    /// <summary>
    /// 非外链
    /// </summary>
    No = 0,

    /// <summary>
    /// 外链
    /// </summary>
    Yes = 1
}

/// <summary>
/// 菜单类型枚举
/// </summary>
public enum MenuType
{
    /// <summary>
    /// 目录
    /// </summary>
    Dir = 0,

    /// <summary>
    /// 菜单
    /// </summary>
    Menu,

    /// <summary>
    /// 按钮
    /// </summary>
    Button
}

/// <summary>
/// api检测缓存操作类型
/// </summary>
public enum CacheOperationType
{
    /// <summary>
    /// 缓存添加
    /// </summary>
    Set,

    /// <summary>
    /// 缓存移除
    /// </summary>
    Remove
}

public enum ErrorStatusCode
{
    UserExist = 1000,
    UserNotFound = 1001,
    PasswordInvalid = 1002,
    PasswordIsNull = 1003,
    ApiNotFound = 1004,
    DeptNotFound = 1005,
    RoleNotFound = 1006,
    MenuNotFound = 1007
}
