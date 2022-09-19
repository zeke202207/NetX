using NetX.SystemManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Core;

public interface IRoleService
{
    Task<List<RoleModel>> GetRoleList();

    Task<List<RoleModel>> GetRoleList(RoleListParam roleListparam);

    Task<bool> AddRole(RoleRequestModel model);

    Task<bool> UpdateRole(RoleRequestModel model);

    Task<bool> UpdateRoleStatus(string roleId,string status);

    Task<bool> RemoveRole(string id);
}
