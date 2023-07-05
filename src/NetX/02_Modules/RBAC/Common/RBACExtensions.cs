using NetX.RBAC.Models;

namespace NetX.RBAC;

/// <summary>
/// 内部扩展方法
/// </summary>
internal static class RBACExtensions
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

    /// <summary>
    /// 菜单树扩展
    /// </summary>
    /// <param name="menus">菜单集合</param>
    /// <param name="parentId">父菜单唯一标识</param>
    /// <returns>菜单树集合</returns>
    public static List<MenuModel> ToTree(this List<MenuModel> menus, string parentId)
    {
        var currentMenus = menus.Where(p => p.ParentId == parentId);
        if (null == currentMenus)
            return new List<MenuModel>();
        foreach (var menu in currentMenus)
        {
            var menuTree = ToTree(menus, menu.Id);
            if (menuTree.Count > 0)
            {
                menu.Children = new List<MenuModel>();
                menu.Children.AddRange(menuTree);
            }
        }
        return currentMenus.ToList();
    }

    /// <summary>
    /// 构建部门树
    /// </summary>
    /// <param name="depts">部门集合</param>
    /// <param name="parentId">父部门编号</param>
    /// <returns></returns>
    public static List<DeptModel> ToTree(this List<DeptModel> depts, string parentId)
    {
        var currentDepts = depts.Where(p => p.ParentId == parentId);
        foreach (var dept in currentDepts)
        {
            var children = ToTree(depts, dept.Id);
            if (children?.Count > 0)
            {
                dept.Children = new List<DeptModel>();
                dept.Children.AddRange(children);
            }
        }
        return currentDepts.ToList();
    }

    /// <summary>
    /// 生成role - api 缓存key
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public static string ToRolePermissionCacheKey(this string roleId)
    {
        return $"{CacheKeys.ACCOUNT_PERMISSIONS}{roleId}";
    }
}
