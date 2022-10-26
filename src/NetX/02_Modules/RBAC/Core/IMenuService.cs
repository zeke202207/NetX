using NetX.Common.Models;
using NetX.RBAC.Models;

namespace NetX.RBAC.Core;

/// <summary>
/// 菜单管理服务接口
/// </summary>
public interface IMenuService
{
    /// <summary>
    /// 获取当前登录用户访问菜单列表
    /// </summary>
    /// <param name="userId">登录用户唯一标识</param>
    /// <returns></returns>
    Task<ResultModel<List<MenuModel>>> GetCurrentUserMenuList(string userId);

    /// <summary>
    /// 获取菜单列表
    /// </summary>
    /// <param name="queryParam">查询条件实体</param>
    /// <returns></returns>
    Task<ResultModel<List<MenuModel>>> GetMenuList(MenuListParam queryParam);

    /// <summary>
    /// 新增菜单
    /// </summary>
    /// <param name="model">菜单实体</param>
    /// <returns></returns>
    Task<ResultModel<bool>> AddMenu(MenuRequestModel model);

    /// <summary>
    /// 更新菜单
    /// </summary>
    /// <param name="model">菜单实体</param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateMenu(MenuRequestModel model);

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="menuId">菜单唯一标识</param>
    /// <returns></returns>
    Task<ResultModel<bool>> RemoveMenu(string menuId);
}
