using Microsoft.Extensions.DependencyInjection;

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
    public static IServiceCollection AddCacheProvider(this IServiceCollection services, Func<IEnumerable<CacheKeyDescriptor>> CacheKeys)
    {
        services.AddSingleton(provider =>
        {
            Func<string, ICacheProvider> func = s =>
                {
                    var cacheProviders = provider.GetServices<ICacheProvider>();
                    var cacheProvider = cacheProviders.FirstOrDefault(p => p.CacheType.Equals(s));
                    if (null == cacheProvider)
                        throw new NotImplementedException($"容器中没有找到对应的缓存提供器，请确认已经注入 {s} 缓存提供器");
                    return cacheProvider;
                };
            return func;
        });
        services.AddSingleton<IEnumerable<CacheKeyDescriptor>>(CacheKeys.Invoke());
        return services;
    }
}
