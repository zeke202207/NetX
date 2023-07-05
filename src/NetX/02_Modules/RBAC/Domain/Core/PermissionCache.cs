using NetX.Cache.Core;
using NetX.Common.Attributes;
using NetX.DatabaseSetup;
using NetX.RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain
{
    [Singleton]
    public sealed class PermissionCache : IPermissionCache
    {
        private readonly ICacheProvider _cache;

        public PermissionCache(ICacheProvider cache) 
        { 
            _cache = cache;
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
