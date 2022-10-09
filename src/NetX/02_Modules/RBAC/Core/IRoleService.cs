using NetX.Common.Models;
using NetX.RBAC.Models;

namespace NetX.RBAC.Core;

/// <summary>
/// 角色管理服务接口
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <returns></returns>
    Task<ResultModel<List<RoleModel>>> GetRoleList();

    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <param name="roleListparam"></param>
    /// <returns></returns>
    Task<ResultModel<List<RoleModel>>> GetRoleList(RoleListParam roleListparam);

    /// <summary>
    /// 添加角色
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> AddRole(RoleRequestModel model);

    /// <summary>
    /// 更新角色信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateRole(RoleRequestModel model);

    /// <summary>
    /// 更新角色状态
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateRoleStatus(string roleId, string status);

    /// <summary>
    /// 更新角色后台鉴权状态
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateRoleApiAuthStatus(string roleId, string status);

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> RemoveRole(string id);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    Task<ResultModel<IEnumerable<string>>> GetApiAuth(string roleId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="apiIds"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateRoleApiAuth(string roleId, IEnumerable<string> apiIds);
}
