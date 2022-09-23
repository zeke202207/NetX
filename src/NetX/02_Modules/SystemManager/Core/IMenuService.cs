using NetX.Common.Models;
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
    Task<ResultModel<List<MenuModel>>> GetCurrentUserMenuList(string userId);

    /// <summary>
    /// 获取菜单列表
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    Task<ResultModel<List<MenuModel>>> GetMenuList(MenuListParam param);

    /// <summary>
    /// 新增菜单
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> AddMenu(MenuRequestModel model);

    /// <summary>
    /// 更新菜单
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateMenu(MenuRequestModel model);

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="menuId"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> RemoveMenu(string menuId);
}
