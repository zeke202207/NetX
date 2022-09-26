using Microsoft.AspNetCore.Mvc;
using NetX.Common.Models;
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
    [ApiActionDescription("获取角色列表")]
    [HttpPost]
    public async Task<ResultModel<List<RoleModel>>> GetRoleListByPage(RoleListParam roleListparam)
    {
        return await _roleService.GetRoleList(roleListparam);
    }

    /// <summary>
    /// 获取全部角色列表
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("获取全部角色列表")]
    [HttpGet]
    public async Task<ResultModel<List<RoleModel>>> GetAllRoleList()
    {
        return await _roleService.GetRoleList();
    }

    /// <summary>
    /// 添加角色
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("添加角色")]
    [HttpPost]
    public async Task<ResultModel<bool>> AddRole(RoleRequestModel model)
    {
        return await _roleService.AddRole(model);
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("更新角色信息")]
    [HttpPost]
    public async Task<ResultModel<bool>> UpdateRole(RoleRequestModel model)
    {
        return await _roleService.UpdateRole(model);
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("删除角色")]
    [HttpDelete]
    public async Task<ResultModel<bool>> RemoveRole(DeleteParam model)
    {
        return await _roleService.RemoveRole(model.Id);
    }

    /// <summary>
    /// 更新角色状态
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("更新角色状态")]
    [HttpPost]
    public async Task<ResultModel<bool>> SetRoleStatus(RoleStatusModel model)
    {
        return await _roleService.UpdateRoleStatus(model.Id, model.Status);
    }

    /// <summary>
    /// 更新角色后台鉴权状态
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("更新角色后台鉴权状态")]
    [HttpPost]
    public async Task<ResultModel<bool>> SetApiCheckStatus(RoleStatusModel model)
    {
        return await _roleService.UpdateRoleApiCheckStatus(model.Id, model.Status);
    }
}
