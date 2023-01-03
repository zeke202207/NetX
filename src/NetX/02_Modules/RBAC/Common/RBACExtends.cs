namespace NetX.RBAC;

/// <summary>
/// 内部扩展方法
/// </summary>
internal static class RBACExtends
{
    /// <summary>
    /// 菜单类型转换
    /// </summary>
    /// <param name="menutype">菜单类型</param>
    /// <returns></returns>
    public static MenuType ToMenuType(this string menutype)
    {
        if (!int.TryParse(menutype, out int intType))
            return MenuType.Dir;
        return (MenuType)intType;
    }
}
