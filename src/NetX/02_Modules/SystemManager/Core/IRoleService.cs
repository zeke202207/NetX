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
}
