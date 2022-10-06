using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Cache.Core;

/// <summary>
/// 
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加缓存
    /// </summary>
    /// <param name="services"></param>
    /// <param name="CacheKeys"></param>
    /// <returns></returns>
    public static IServiceCollection AddCache<T>(this IServiceCollection services, Func<IEnumerable<CacheKeyDescriptor>> CacheKeys)
        where T : class, ICacheProvider
    {
        services.AddSingleton<IMemoryCache>(factory => new MemoryCache(new MemoryCacheOptions()));
        services.AddSingleton<ICacheProvider, T>();
        services.AddSingleton<IEnumerable<CacheKeyDescriptor>>(CacheKeys.Invoke());
        return services;
    }
}
