using FreeSql;
using NetX.Authentication.Core;
using NetX.Common;
using NetX.Common.Attributes;
using NetX.SystemManager.Models;
using NetX.SystemManager.Models.Dtos.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Core;

[Scoped]
public class RoleService : IRoleService
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
        var roles = this._roleRepository.Select
            .WhereIf(!string.IsNullOrWhiteSpace(roleListparam.RoleName), p => p.rolename.Equals(roleListparam.RoleName))
            .WhereIf(int.TryParse(roleListparam.Status, out int _), p => p.status == int.Parse(roleListparam.Status));
        if (roleListparam.Page >= 0 && roleListparam.PageSize > 0)
            roles.Page(roleListparam.Page, roleListparam.PageSize);
        return (await roles.ToListAsync()).ConvertAll<RoleModel>(p => new RoleModel()
        {
            Id = p.id,
            CreateTime = p.createtime,
            OrderNo = p.orderno.ToString(),
            Remark = p.remark,
            RoleName = p.rolename,
            RoleValue = p.rolevalue,
            Status = p.status.ToString()
        });
    }
}
