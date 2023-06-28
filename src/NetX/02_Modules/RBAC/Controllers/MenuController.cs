using Microsoft.AspNetCore.Mvc;
using Netx.Ddd.Core;
using NetX.AuditLog;
using NetX.Authentication.Core;
using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Domain;
using NetX.RBAC.Models;
using NetX.Swagger;
using NetX.Tenants;

namespace NetX.RBAC.Controllers;

/// <summary>
/// 菜单管理api接口
/// </summary>
[ApiControllerDescription(RBACConst.C_RBAC_GROUPNAME, Description = "NetX实现的系统管理模块->菜单管理")]
public class MenuController : RBACBaseController
{
    private readonly IQueryBus _menuQuery;
    private readonly ICommandBus _menuCommand;

    /// <summary>
    /// 菜单管理api接口实例对象
    /// </summary>
    public MenuController(IQueryBus menuQuery, ICommandBus menuCommand)
    {
        this._menuQuery = menuQuery;
        this._menuCommand = menuCommand;
    }

    /// <summary>
    /// 获取登录用户授权菜单列表
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("获取登录用户授权菜单列表")]
    [NoPermission]
    [HttpGet]
    public async Task<ResultModel> GetCurrentUserMenuList()
    {
        return await this._menuQuery.Send<MenuCurrentUserQuery, ResultModel>(new MenuCurrentUserQuery(TenantContext.CurrentTenant.Principal?.UserId ?? string.Empty));
    }

    /// <summary>
    /// 根据条件获取菜单列表
    /// </summary>
    /// <param name="param">获取条件实体</param>
    /// <returns></returns>
    [ApiActionDescription("根据条件获取菜单列表")]
    [NoPermission]
    [HttpPost]
    public async Task<ResultModel> GetMenuList(MenuListParam param)
    {
        return await this._menuQuery.Send<MenuPagerListQuery, ResultModel>(new MenuPagerListQuery(param.Title, param.Status, param.CurrentPage, param.PageSize));
    }

    /// <summary>
    /// 添加菜单
    /// </summary>
    /// <param name="model">菜单实体对象</param>
    /// <returns></returns>
    [Audited]
    [ApiActionDescription("添加菜单")]
    [HttpPost]
    public async Task<ResultModel> AddMenu(MenuRequestModel model)
    {
        await _menuCommand.Send<MenuAddCommand>(new MenuAddCommand(model.ParentId, model.Path, model.Title, model.Component, model.Redirect, model.Meta, model.Icon, model.Type, model.Permission, model.OrderNo, model.Status, model.IsExt, model.Show, model.ExtPath));
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 编辑菜单
    /// </summary>
    /// <param name="model">菜单实体对象</param>
    /// <returns></returns>
    [Audited]
    [ApiActionDescription("编辑菜单")]
    [HttpPost]
    public async Task<ResultModel> UpdateMenu(MenuRequestModel model)
    {
        await _menuCommand.Send<MenuModifyCommand>(new MenuModifyCommand(model.Id, model.ParentId, model.Path, model.Title, model.Component, model.Redirect, model.Meta, model.Icon, model.Type, model.Permission, model.OrderNo, model.Status, model.IsExt, model.Show, model.ExtPath));
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="param">删除实体对象</param>
    /// <returns></returns>
    [Audited]
    [ApiActionDescription("删除菜单")]
    [HttpDelete]
    public async Task<ResultModel> RemoveMenu(KeyParam param)
    {
        await _menuCommand.Send<MenuRemoveCommand>(new MenuRemoveCommand(param.Id));
        return true.ToSuccessResultModel();
    }
}
