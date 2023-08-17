using NetX.RBAC.Models;

namespace NetX.RBAC.Domain
{
    public interface IPermissionCache
    {
        /// <summary>
        /// 角色权限缓存是否存在
        /// </summary>
        /// <param name="key">缓存key<see cref="RBACExtensions.ToRolePermissionCacheKey"/></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string key);

        /// <summary>
        /// 获取角色权限缓存
        /// </summary>
        /// <param name="key">缓存key<see cref="RBACExtensions.ToRolePermissionCacheKey"/></param>
        /// <returns></returns>
        Task<PermissionCacheModel> GetAsync(string key);

        /// <summary>
        /// 删除角色权限缓存
        /// </summary>
        /// <param name="key">缓存key<see cref="RBACExtensions.ToRolePermissionCacheKey"/></param>
        /// <returns></returns>
        Task<bool> RemoveAsync(string key);

        /// <summary>
        /// 设置角色权限缓存
        /// </summary>
        /// <param name="key">缓存key<see cref="RBACExtensions.ToRolePermissionCacheKey"/></param>
        /// <param name="cacheModel">缓存内容</param>
        /// <returns></returns>
        Task<bool> SetAsync(string key, PermissionCacheModel cacheModel);
    }
}
