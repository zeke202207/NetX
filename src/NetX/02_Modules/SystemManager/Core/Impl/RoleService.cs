using FreeSql;
using Microsoft.AspNetCore.JsonPatch.Internal;
using NetX.Authentication.Core;
using NetX.Common;
using NetX.Common.Attributes;
using NetX.SystemManager.Core.Impl;
using NetX.SystemManager.Data.Repositories;
using NetX.SystemManager.Models;
using NetX.SystemManager.Models.Dtos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Core;

[Scoped]
public class RoleService : BaseService, IRoleService
{
    private readonly IBaseRepository<sys_role> _roleRepository;

    public RoleService(
        IBaseRepository<sys_role> roleRepository)
    {
        this._roleRepository = roleRepository;
    }
    public async Task<List<RoleModel>> GetRoleList()
    {
        return await GetRoleList(new RoleListParam());
    }

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
        return ((SysRoleRepository)_roleRepository).AddRole(roleEntity, model.Menus);
    }

    public Task<bool> UpdateRole(RoleRequestModel model)
    {
        var roleEntity = new sys_role()
        {
            id = model.Id,
            createtime = base.CreateInsertTime(),
            rolename = model.RoleName,
            status = int.Parse(model.Status),
            remark = model?.Remark
        };
        return ((SysRoleRepository)_roleRepository).UpdateRole(roleEntity, model.Menus);
    }

    public Task<bool> RemoveRole(string roleId)
    {
        return ((SysRoleRepository)_roleRepository).RemoveRole(roleId);
    }

    public async Task<bool> UpdateRoleStatus(string roleId, string status)
    {
        int intStatus = 0;
        if (!int.TryParse(status, out intStatus))
            return false;
        var roleEntity = await _roleRepository.Select.Where(p => p.id.Equals(roleId)).FirstAsync() ;
        if (null == roleEntity)
            return false;
        roleEntity.status = intStatus;
        await _roleRepository.UpdateAsync(roleEntity);
        return true;
    }
}
