using NetX.SystemManager.Models;

namespace NetX.SystemManager.Core;

/// <summary>
/// 角色管理服务接口
/// </summary>
public interface IRoleService
{
    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <returns></returns>
    Task<List<RoleModel>> GetRoleList();

    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <param name="roleListparam"></param>
    /// <returns></returns>
    Task<List<RoleModel>> GetRoleList(RoleListParam roleListparam);

    /// <summary>
    /// 添加角色
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<bool> AddRole(RoleRequestModel model);

    /// <summary>
    /// 更新角色信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<bool> UpdateRole(RoleRequestModel model);

    /// <summary>
    /// 更新角色状态
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    Task<bool> UpdateRoleStatus(string roleId, string status);

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> RemoveRole(string id);
}
