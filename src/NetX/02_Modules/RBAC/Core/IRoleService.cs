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
    /// <param name="queryParam">查询条件实体</param>
    /// <returns></returns>
    Task<ResultModel<List<RoleModel>>> GetRoleList(RoleListParam queryParam);

    /// <summary>
    /// 添加角色
    /// </summary>
    /// <param name="model">角色实体</param>
    /// <returns></returns>
    Task<ResultModel<bool>> AddRole(RoleRequestModel model);

    /// <summary>
    /// 更新角色信息
    /// </summary>
    /// <param name="model">角色实体</param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateRole(RoleRequestModel model);

    /// <summary>
    /// 更新角色状态
    /// </summary>
    /// <param name="roleId">角色唯一标识</param>
    /// <param name="status">角色启用状态</param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateRoleStatus(string roleId, string status);

    /// <summary>
    /// 更新角色后台鉴权状态
    /// </summary>
    /// <param name="roleId">角色唯一标识</param>
    /// <param name="status">后台api鉴权启用状态</param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateRoleApiAuthStatus(string roleId, string status);

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="id">角色唯一标识</param>
    /// <returns></returns>
    Task<ResultModel<bool>> RemoveRole(string id);

    /// <summary>
    /// 获取角色已授权访问api集合
    /// </summary>
    /// <param name="roleId">角色唯一标识</param>
    /// <returns></returns>
    Task<ResultModel<IEnumerable<string>>> GetApiAuth(string roleId);

    /// <summary>
    /// 给角色授权后台访问api集合
    /// </summary>
    /// <param name="roleId">角色唯一标识</param>
    /// <param name="apiIds">已授权后台api访问集合</param>
    /// <returns></returns>
    Task<ResultModel<bool>> UpdateRoleApiAuth(string roleId, IEnumerable<string> apiIds);
}
