using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using NetX.Cache.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.InMemoryCache;

/// <summary>
/// 缓冲注入类
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加缓存
    /// </summary>
    /// <param name="services"></param>
    /// <param name="CacheKeys"></param>
    /// <returns></returns>
    public static IServiceCollection AddInMemoryCache(this IServiceCollection services, Func<IEnumerable<CacheKeyDescriptor>> CacheKeys)
    {
        services.AddSingleton<IMemoryCache>(factory => new MemoryCache(new MemoryCacheOptions()));
        services.AddSingleton<ICacheProvider, MemoryCacheProvider>();
        services.AddSingleton<IEnumerable<CacheKeyDescriptor>>(CacheKeys.Invoke());
        return services;
    }
}
