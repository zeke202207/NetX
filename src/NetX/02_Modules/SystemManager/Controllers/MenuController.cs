using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Swagger;
using NetX.SystemManager.Core;
using NetX.SystemManager.Models;
using NetX.Tenants;

namespace NetX.SystemManager.Controllers;

/// <summary>
/// 菜单管理api接口
/// </summary>
[ApiControllerDescription("SystemManager", Description = "NetX实现的系统管理模块->菜单管理")]
public class MenuController : SystemManagerBaseController
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
    [ApiActionDescriptionAttribute("获取登录用户授权菜单列表")]
    [NoPermission]
    [HttpGet]
    public async Task<ActionResult> GetCurrentUserMenuList()
    {
        var result = await this._menuService.GetCurrentUserMenuList(TenantContext.CurrentTenant.Principal?.UserId);
        if (null == result)
            return base.Error(Common.Models.ResultEnum.ERROR, "获取当前登录用户授权菜单列表失败");
        return base.Success<List<MenuModel>>(result);
    }

    /// <summary>
    /// 获取菜单列表
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    [ApiActionDescriptionAttribute("获取菜单列表")]
    [NoPermission]
    [HttpPost]
    public async Task<ActionResult> GetMenuList(MenuListParam param)
    {
        var result = await this._menuService.GetMenuList(param);
        return base.Success<List<MenuModel>>(result);
    }

    /// <summary>
    /// 添加菜单
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [ApiActionDescriptionAttribute("添加菜单")]
    [HttpPost]
    public async Task<ActionResult> AddMenu(MenuRequestModel model)
    {
        var result = await this._menuService.AddMenu(model);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 编辑菜单
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [ApiActionDescriptionAttribute("编辑菜单")]
    [HttpPost]
    public async Task<ActionResult> UpdateMenu(MenuRequestModel model)
    {
        var result = await this._menuService.UpdateMenu(model);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    [ApiActionDescriptionAttribute("删除菜单")]
    [HttpDelete]
    public async Task<ActionResult> RemoveMenu(DeleteParam param)
    {
        var result = await this._menuService.RemoveMenu(param.Id);
        return base.Success<bool>(result);
    }
}
