using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common.Models;
using NetX.Swagger;
using NetX.RBAC.Core;
using NetX.RBAC.Models;
using NetX.Tenants;
using NetX.Logging.Monitors;

namespace NetX.RBAC.Controllers;

/// <summary>
/// 菜单管理api接口
/// </summary>
[ApiControllerDescription(RBACConst.C_RBAC_GROUPNAME, Description = "NetX实现的系统管理模块->菜单管理")]
public class MenuController : RBACBaseController
{
    private IMenuService _menuService;

    /// <summary>
    /// 菜单管理api接口实例对象
    /// </summary>
    /// <param name="menuService"></param>
    public MenuController(IMenuService menuService)
    {
        this._menuService = menuService;
    }

    /// <summary>
    /// 获取登录用户授权菜单列表
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("获取登录用户授权菜单列表")]
    [NoPermission]
    [HttpGet]
    public async Task<ResultModel<List<MenuModel>>> GetCurrentUserMenuList()
    {
        return await this._menuService.GetCurrentUserMenuList(TenantContext.CurrentTenant.Principal?.UserId);
    }

    /// <summary>
    /// 获取菜单列表
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    [ApiActionDescription("获取菜单列表")]
    [NoPermission]
    [HttpPost]
    public async Task<ResultModel<List<MenuModel>>> GetMenuList(MenuListParam param)
    {
        return await this._menuService.GetMenuList(param);
    }

    /// <summary>
    /// 添加菜单
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("添加菜单")]
    [HttpPost]
    public async Task<ResultModel<bool>> AddMenu(MenuRequestModel model)
    {
        return await this._menuService.AddMenu(model);
    }

    /// <summary>
    /// 编辑菜单
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("编辑菜单")]
    [HttpPost]
    public async Task<ResultModel<bool>> UpdateMenu(MenuRequestModel model)
    {
        return await this._menuService.UpdateMenu(model);
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("删除菜单")]
    [HttpDelete]
    public async Task<ResultModel<bool>> RemoveMenu(KeyParam param)
    {
        return await this._menuService.RemoveMenu(param.Id);
    }
}
