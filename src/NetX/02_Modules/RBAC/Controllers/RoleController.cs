using Microsoft.AspNetCore.Mvc;
using NetX.Common.ModuleInfrastructure;
using NetX.Swagger;
using NetX.RBAC.Core;
using NetX.RBAC.Models;
using NetX.Logging.Monitors;

namespace NetX.RBAC.Controllers;

/// <summary>
/// 角色管理api接口
/// </summary>
[ApiControllerDescription(RBACConst.C_RBAC_GROUPNAME, Description = "NetX实现的系统管理模块->角色管理")]
public class RoleController : RBACBaseController
{
    private IRoleService _roleService;

    /// <summary>
    /// 角色管理api实例对象
    /// </summary>
    /// <param name="roleService">角色服务</param>
    public RoleController(IRoleService roleService)
    {
        this._roleService = roleService;
    }

    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <param name="roleListparam">角色筛选实体对象</param>
    /// <returns></returns>
    [ApiActionDescription("获取角色列表")]
    [HttpPost]
    public async Task<ResultModel> GetRoleListByPage(RoleListParam roleListparam)
    {
        return await _roleService.GetRoleList(roleListparam);
    }

    /// <summary>
    /// 获取全部角色列表
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("获取全部角色列表")]
    [HttpGet]
    public async Task<ResultModel> GetAllRoleList()
    {
        return await _roleService.GetRoleList();
    }

    /// <summary>
    /// 添加角色
    /// </summary>
    /// <param name="model">角色实体对象</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("添加角色")]
    [HttpPost]
    public async Task<ResultModel> AddRole(RoleRequestModel model)
    {
        return await _roleService.AddRole(model);
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="model">角色实体对象</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("更新角色信息")]
    [HttpPost]
    public async Task<ResultModel> UpdateRole(RoleRequestModel model)
    {
        return await _roleService.UpdateRole(model);
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="model">删除实体</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("删除角色")]
    [HttpDelete]
    public async Task<ResultModel> RemoveRole(KeyParam model)
    {
        return await _roleService.RemoveRole(model.Id);
    }

    /// <summary>
    /// 更新角色状态
    /// </summary>
    /// <param name="model">角色状态实体对象</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("更新角色状态")]
    [HttpPost]
    public async Task<ResultModel> SetRoleStatus(RoleStatusModel model)
    {
        return await _roleService.UpdateRoleStatus(model.Id, model.Status);
    }

    /// <summary>
    /// 更新角色后台鉴权状态
    /// </summary>
    /// <param name="model">橘色状态实体对象</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("更新角色后台鉴权状态")]
    [HttpPost]
    public async Task<ResultModel> SetApiAuthStatus(RoleStatusModel model)
    {
        return await _roleService.UpdateRoleApiAuthStatus(model.Id, model.Status);
    }

    /// <summary>
    /// 获取后台api授权id集合
    /// </summary>
    /// <param name="param">角色id实体对象</param>
    /// <returns></returns>
    [ApiActionDescription("获取后台api授权id集合")]
    [HttpPost]
    public async Task<ResultModel> GetApiAuth(KeyParam param)
    {
        return await _roleService.GetApiAuth(param.Id);
    }

    /// <summary>
    /// 更新角色后台鉴权状态
    /// </summary>
    /// <param name="model">角色api鉴权集合</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("设置后台api授权id集合")]
    [HttpPost]
    public async Task<ResultModel> SetApiAuth(RoleApiAuthModel model)
    {
        return await _roleService.UpdateRoleApiAuth(model.RoleId, model.ApiIds);
    }
}
