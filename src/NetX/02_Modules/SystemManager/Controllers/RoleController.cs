using Microsoft.AspNetCore.Mvc;
using NetX.Swagger;
using NetX.SystemManager.Core;
using NetX.SystemManager.Models;

namespace NetX.SystemManager.Controllers;

/// <summary>
/// 角色管理api接口
/// </summary>
[ApiControllerDescription("SystemManager", Description = "NetX实现的系统管理模块->角色管理")]
public class RoleController : SystemManagerBaseController
{
    private IRoleService _roleService;

    /// <summary>
    /// 角色管理api实例对象
    /// </summary>
    /// <param name="roleService"></param>
    public RoleController(IRoleService roleService)
    {
        this._roleService = roleService;
    }

    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <param name="roleListparam"></param>
    /// <returns></returns>
    [ApiActionDescriptionAttribute("获取角色列表")]
    [HttpPost]
    public async Task<ActionResult> GetRoleListByPage(RoleListParam roleListparam)
    {
        var result = await _roleService.GetRoleList(roleListparam);
        return base.Success<List<RoleModel>>(result);
    }

    /// <summary>
    /// 获取全部角色列表
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("获取全部角色列表")]
    [HttpGet]
    public async Task<ActionResult> GetAllRoleList()
    {
        var result = await _roleService.GetRoleList();
        return base.Success<List<RoleModel>>(result);
    }

    /// <summary>
    /// 添加角色
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("添加角色")]
    [HttpPost]
    public async Task<ActionResult> AddRole(RoleRequestModel model)
    {
        var result = await _roleService.AddRole(model);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("更新角色信息")]
    [HttpPost]
    public async Task<ActionResult> UpdateRole(RoleRequestModel model)
    {
        var result = await _roleService.UpdateRole(model);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("删除角色")]
    [HttpDelete]
    public async Task<ActionResult> RemoveRole(DeleteParam model)
    {
        var result = await _roleService.RemoveRole(model.Id);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 更新角色状态
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("更新角色状态")]
    [HttpPost]
    public async Task<ActionResult> SetRoleStatus(RoleStatusModel model)
    {
        var result = await _roleService.UpdateRoleStatus(model.Id, model.Status);
        return base.Success<bool>(result);
    }
}
