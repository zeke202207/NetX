using Microsoft.AspNetCore.Mvc;
using NetX.Common.Models;
using NetX.Swagger;
using NetX.RBAC.Core;
using NetX.RBAC.Models;

namespace NetX.RBAC.Controllers;

/// <summary>
/// 角色管理api接口
/// </summary>
[ApiControllerDescription("RBAC", Description = "NetX实现的系统管理模块->角色管理")]
public class RoleController : RBACBaseController
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
    public async Task<ResultModel<bool>> RemoveRole(KeyParam model)
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
    public async Task<ResultModel<bool>> SetApiAuthStatus(RoleStatusModel model)
    {
        return await _roleService.UpdateRoleApiAuthStatus(model.Id, model.Status);
    }

    /// <summary>
    /// 获取后台api授权id集合
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    [ApiActionDescription("获取后台api授权id集合")]
    [HttpPost]
    public async Task<ResultModel<IEnumerable<string>>> GetApiAuth(KeyParam param)
    {
        return await _roleService.GetApiAuth(param.Id);
    }

    /// <summary>
    /// 更新角色后台鉴权状态
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("设置后台api授权id集合")]
    [HttpPost]
    public async Task<ResultModel<bool>> SetApiAuth(RoleApiAuthModel model)
    {
        return await _roleService.UpdateRoleApiAuth(model.RoleId, model.ApiIds);
    }
}
