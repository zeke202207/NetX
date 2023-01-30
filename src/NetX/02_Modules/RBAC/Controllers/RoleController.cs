using Microsoft.AspNetCore.Mvc;
using Netx.Ddd.Core;
using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Domain;
using NetX.RBAC.Models;
using NetX.Swagger;

namespace NetX.RBAC.Controllers;

/// <summary>
/// 角色管理api接口
/// </summary>
[ApiControllerDescription(RBACConst.C_RBAC_GROUPNAME, Description = "NetX实现的系统管理模块->角色管理")]
public class RoleController : RBACBaseController
{
    private readonly IQueryBus _roleQuery;
    private readonly ICommandBus _roleCommand;

    /// <summary>
    /// 角色管理api实例对象
    /// </summary>
    public RoleController(IQueryBus roleQuery, ICommandBus roleCommand)
    {
        this._roleQuery = roleQuery;
        this._roleCommand = roleCommand;
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
        return await _roleQuery.Send<RolePagerListQuery, ResultModel>(new RolePagerListQuery(roleListparam.RoleName, roleListparam.Status, roleListparam.CurrentPage, roleListparam.PageSize));
    }

    /// <summary>
    /// 获取全部角色列表
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("获取全部角色列表")]
    [HttpGet]
    public async Task<ResultModel> GetAllRoleList()
    {
        return await _roleQuery.Send<RolePagerListQuery, ResultModel>(new RolePagerListQuery(string.Empty, string.Empty, 0, int.MaxValue));
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
        return await _roleQuery.Send<RoleApiQuery, ResultModel>(new RoleApiQuery(param.Id));
    }

    /// <summary>
    /// 添加角色
    /// </summary>
    /// <param name="model">角色实体对象</param>
    /// <returns></returns>
    //[Audit]
    [ApiActionDescription("添加角色")]
    [HttpPost]
    public async Task<ResultModel> AddRole(RoleRequestModel model)
    {
        await _roleCommand.Send<RoleAddCommand>(new RoleAddCommand(model.RoleName, model.Status, model.ApiCheck, model.Remark, model.ToMenuList()));
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="model">角色实体对象</param>
    /// <returns></returns>
    //[Audit]
    [ApiActionDescription("更新角色信息")]
    [HttpPost]
    public async Task<ResultModel> UpdateRole(RoleRequestModel model)
    {
        await _roleCommand.Send<RoleModifyCommand>(new RoleModifyCommand(model.Id, model.RoleName, model.Status, model.ApiCheck, model.Remark, model.ToMenuList()));
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="model">删除实体</param>
    /// <returns></returns>
    //[Audit]
    [ApiActionDescription("删除角色")]
    [HttpDelete]
    public async Task<ResultModel> RemoveRole(KeyParam model)
    {
        await _roleCommand.Send<RoleRemoveCommand>(new RoleRemoveCommand(model.Id));
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 更新角色状态
    /// </summary>
    /// <param name="model">角色状态实体对象</param>
    /// <returns></returns>
    //[Audit]
    [ApiActionDescription("更新角色状态")]
    [HttpPost]
    public async Task<ResultModel> SetRoleStatus(RoleStatusModel model)
    {
        await _roleCommand.Send<RoleStatusModifyCommand>(new RoleStatusModifyCommand(model.Id, model.Status));
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 更新角色后台鉴权状态
    /// </summary>
    /// <param name="model">橘色状态实体对象</param>
    /// <returns></returns>
    //[Audit]
    [ApiActionDescription("更新角色后台鉴权状态")]
    [HttpPost]
    public async Task<ResultModel> SetApiAuthStatus(RoleStatusModel model)
    {
        await _roleCommand.Send<RoleApiAuthModifyCommand>(new RoleApiAuthModifyCommand(model.Id, model.Status));
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 更新角色后台鉴权状态
    /// </summary>
    /// <param name="model">角色api鉴权集合</param>
    /// <returns></returns>
    //[Audit]
    [ApiActionDescription("设置后台api授权id集合")]
    [HttpPost]
    public async Task<ResultModel> SetApiAuth(RoleApiAuthModel model)
    {
        await _roleCommand.Send<RoleApiAuthSettingCommand>(new RoleApiAuthSettingCommand(model.RoleId, model.ApiIds));
        return true.ToSuccessResultModel();
    }
}
