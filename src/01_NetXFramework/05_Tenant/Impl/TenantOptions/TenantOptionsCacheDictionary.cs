﻿using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace NetX.Tenants;

/// <summary>
/// 租户选项缓存字典包装器
/// </summary>
internal class TenantOptionsCacheDictionary<TOptions> where TOptions : class
{
    /// <summary>
    /// Caches stored in memory
    /// </summary>
    private readonly ConcurrentDictionary<string, IOptionsMonitorCache<TOptions>> _tenantSpecificOptionCaches =
        new ConcurrentDictionary<string, IOptionsMonitorCache<TOptions>>();

    /// <summary>
    /// Get options for specific tenant (create if not exists)
    /// </summary>
    /// <param name="tenantId"></param>
    /// <returns></returns>
    public IOptionsMonitorCache<TOptions> Get(string tenantId)
    {
        return _tenantSpecificOptionCaches.GetOrAdd(tenantId, new OptionsCache<TOptions>());
    }
}
