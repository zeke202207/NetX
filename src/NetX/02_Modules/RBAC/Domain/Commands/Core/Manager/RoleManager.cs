using NetX.Common.Attributes;

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
