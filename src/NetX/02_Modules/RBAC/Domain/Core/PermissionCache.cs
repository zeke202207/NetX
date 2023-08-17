using NetX.Cache.Core;
using NetX.Common.Attributes;
using NetX.InMemoryCache;
using NetX.RBAC.Models;

namespace NetX.RBAC.Domain
{
    [Singleton]
    public sealed class PermissionCache : IPermissionCache
    {
        private readonly ICacheProvider _cache;

        public PermissionCache(Func<string, ICacheProvider> funcFactory)
        {
            _cache = funcFactory(InMemoryCacheConstEnum.C_CACHE_TYPE_KEY);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _cache.ExistsAsync<PermissionCacheModel>(key);
        }

        public async Task<PermissionCacheModel> GetAsync(string key)
        {
            return await _cache.GetAsync<PermissionCacheModel>(key);
        }

        public async Task<bool> RemoveAsync(string key)
        {
            return await _cache.RemoveAsync(key);
        }

        public async Task<bool> SetAsync(string key, PermissionCacheModel cacheModel)
        {
            return await _cache.SetAsync<PermissionCacheModel>(key, cacheModel, TimeSpan.FromHours(2));
        }
    }
}
