namespace NetX.SystemManager;

internal class SystemManagerConst
{
    public const string C_ROOT_ID = "00000000000000000000000000000000";
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
/// 
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
