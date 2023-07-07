using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain.Commands
{
    public interface IRoleManager
    {
        Task<bool> RemovePermissionCacheAsync(string roleId);
    }
}
