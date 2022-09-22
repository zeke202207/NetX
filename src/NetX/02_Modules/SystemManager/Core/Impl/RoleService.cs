using FreeSql;
using NetX.Common.Attributes;
using NetX.SystemManager.Data.Repositories;
using NetX.SystemManager.Models;

namespace NetX.SystemManager.Core;

/// <summary>
/// 角色管理服务
/// </summary>
[Scoped]
public class RoleService : BaseService, IRoleService
{
    private readonly IBaseRepository<sys_role> _roleRepository;

    /// <summary>
    /// 角色管理服务实例对象
    /// </summary>
    /// <param name="roleRepository"></param>
    public RoleService(
        IBaseRepository<sys_role> roleRepository)
    {
        this._roleRepository = roleRepository;
    }

    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<RoleModel>> GetRoleList()
    {
        return await GetRoleList(new RoleListParam());
    }

    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <param name="roleListparam"></param>
    /// <returns></returns>
    public async Task<List<RoleModel>> GetRoleList(RoleListParam roleListparam)
    {
        var roles = await ((SysRoleRepository)this._roleRepository)
            .GetRoleList(roleListparam.RoleName, roleListparam.Page, roleListparam.PageSize);
        return roles.ConvertAll<RoleModel>(p => new RoleModel()
        {
            Id = p.role.id,
            CreateTime = p.role.createtime,
            Remark = p.role.remark,
            RoleName = p.role.rolename,
            Status = p.role.status.ToString(),
            Menus = p.menuids
        });
    }

    /// <summary>
    /// 添加角色
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public Task<bool> AddRole(RoleRequestModel model)
    {
        var roleEntity = new sys_role()
        {
            id = base.CreateId(),
            createtime = base.CreateInsertTime(),
            rolename = model.RoleName,
            status = int.Parse(model.Status),
            remark = model?.Remark
        };
        return ((SysRoleRepository)_roleRepository).AddRole(roleEntity, model.ToMenuList());
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<bool> UpdateRole(RoleRequestModel model)
    {
        var roleEntity = await _roleRepository.Select.Where(p => p.id.Equals(model.Id)).FirstAsync();
        roleEntity.createtime = base.CreateInsertTime();
        roleEntity.rolename = model.RoleName;
        roleEntity.status = int.Parse(model.Status);
        roleEntity.remark = model?.Remark;
        return await ((SysRoleRepository)_roleRepository).UpdateRole(roleEntity, model.ToMenuList());
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public Task<bool> RemoveRole(string roleId)
    {
        return ((SysRoleRepository)_roleRepository).RemoveRole(roleId);
    }

    /// <summary>
    /// 更新角色状态
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<bool> UpdateRoleStatus(string roleId, string status)
    {
        int intStatus = 0;
        if (!int.TryParse(status, out intStatus))
            return false;
        var roleEntity = await _roleRepository.Select.Where(p => p.id.Equals(roleId)).FirstAsync();
        if (null == roleEntity)
            return false;
        roleEntity.status = intStatus;
        await _roleRepository.UpdateAsync(roleEntity);
        return true;
    }
}
