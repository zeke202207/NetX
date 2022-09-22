using NetX.SystemManager.Models;

namespace NetX.SystemManager.Core;

/// <summary>
/// 菜单管理服务接口
/// </summary>
public interface IMenuService
{
    /// <summary>
    /// 获取当前登录用户访问菜单列表
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    Task<List<MenuModel>> GetCurrentUserMenuList(string? userId);

    /// <summary>
    /// 获取菜单列表
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    Task<List<MenuModel>> GetMenuList(MenuListParam param);

    /// <summary>
    /// 新增菜单
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<bool> AddMenu(MenuRequestModel model);

    /// <summary>
    /// 更新菜单
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<bool> UpdateMenu(MenuRequestModel model);

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    Task<bool> RemoveMenu(string menuId);
}
