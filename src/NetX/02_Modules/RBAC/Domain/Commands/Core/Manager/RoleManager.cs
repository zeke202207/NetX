using MediatR;
using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain.Commands
{
    [Scoped]
    public class RoleManager : IRoleManager
    {

        private readonly IPermissionCache _cacheManager;

        public RoleManager(IPermissionCache cacheManager)
        {
            _cacheManager = cacheManager;
        }

        public async Task<bool> RemovePermissionCacheAsync(string roleId)
        {
            return await _cacheManager.RemoveAsync(roleId.ToRolePermissionCacheKey());
        }
    }
}
