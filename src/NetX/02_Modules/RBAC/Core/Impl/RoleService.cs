using AutoMapper;
using FreeSql;
using NetX.Common.Attributes;
using NetX.Common.Models;
using NetX.RBAC.Data.Repositories;
using NetX.RBAC.Models;

namespace NetX.RBAC.Core;

/// <summary>
/// 角色管理服务
/// </summary>
[Scoped]
public class RoleService : RBACBaseService, IRoleService
{
    private readonly IBaseRepository<sys_role> _roleRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 角色管理服务实例对象
    /// </summary>
    /// <param name="roleRepository"></param>
    /// <param name="mapper"></param>
    public RoleService(
        IBaseRepository<sys_role> roleRepository,
        IMapper mapper)
    {
        this._roleRepository = roleRepository;
        this._mapper = mapper;
    }

    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <returns></returns>
    public async Task<ResultModel<List<RoleModel>>> GetRoleList()
    {
        return await GetRoleList(new RoleListParam());
    }

    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <param name="roleListparam"></param>
    /// <returns></returns>
    public async Task<ResultModel<List<RoleModel>>> GetRoleList(RoleListParam roleListparam)
    {
        var roles = await ((SysRoleRepository)this._roleRepository)
            .GetRoleListAsync(roleListparam.RoleName, roleListparam.Page, roleListparam.PageSize);
        var result = this._mapper.Map<List<RoleModel>>(roles);
        return base.Success<List<RoleModel>>(result);
    }

    /// <summary>
    /// 添加角色
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> AddRole(RoleRequestModel model)
    {
        var roleEntity = new sys_role()
        {
            id = base.CreateId(),
            createtime = base.CreateInsertTime(),
            rolename = model.RoleName,
            status = int.Parse(model.Status),
            remark = model.Remark ?? ""
        };
        var result = await ((SysRoleRepository)_roleRepository).AddRoleAsync(roleEntity, model.ToMenuList());
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> UpdateRole(RoleRequestModel model)
    {
        var roleEntity = await _roleRepository.Select.Where(p => p.id.Equals(model.Id)).FirstAsync();
        roleEntity.createtime = base.CreateInsertTime();
        roleEntity.rolename = model.RoleName;
        roleEntity.status = int.Parse(model.Status);
        roleEntity.remark = model?.Remark;
        var result = await ((SysRoleRepository)_roleRepository).UpdateRoleAsync(roleEntity, model.ToMenuList());
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> RemoveRole(string roleId)
    {
        var result = await ((SysRoleRepository)_roleRepository).RemoveRoleAsync(roleId);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 更新角色状态
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> UpdateRoleStatus(string roleId, string status)
    {
        int intStatus = 0;
        if (!int.TryParse(status, out intStatus))
            return base.Error<bool>("输入状态值无效");
        var roleEntity = await _roleRepository.Select.Where(p => p.id.Equals(roleId)).FirstAsync();
        if (null == roleEntity)
            return base.Error<bool>("角色不存在");
        roleEntity.status = intStatus;
        await _roleRepository.UpdateAsync(roleEntity);
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 更新角色后台鉴权状态
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> UpdateRoleApiAuthStatus(string roleId, string status)
    {
        int intStatus = 0;
        if (!int.TryParse(status, out intStatus))
            return base.Error<bool>("输入状态值无效");
        var roleEntity = await _roleRepository.Select.Where(p => p.id.Equals(roleId)).FirstAsync();
        if (null == roleEntity)
            return base.Error<bool>("角色不存在");
        roleEntity.apicheck = intStatus;
        await _roleRepository.UpdateAsync(roleEntity);
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 获取角色后台api访问权限集合
    /// </summary>
    /// <param name="roleId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<IEnumerable<string>>> GetApiAuth(string roleId)
    {
        var result = await ((SysRoleRepository)this._roleRepository).GetApiAuth(roleId);
        return base.Success<IEnumerable<string>>(result);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="roleId"></param>
    /// <param name="apiIds"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> UpdateRoleApiAuth(string roleId, IEnumerable<string> apiIds)
    {
        var result = await ((SysRoleRepository)this._roleRepository).UpdateRoleApiAuth(roleId, apiIds);
        return base.Success<bool>(result);
    }
}
