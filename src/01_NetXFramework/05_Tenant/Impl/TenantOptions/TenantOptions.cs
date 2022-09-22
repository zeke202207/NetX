using Microsoft.Extensions.Options;

namespace NetX.Tenants;

/// <summary>
/// Make IOptions tenant aware
/// </summary>
public class TenantOptions<TOptions> :
    IOptions<TOptions>, IOptionsSnapshot<TOptions> where TOptions : class, new()
{
    private readonly IOptionsFactory<TOptions> _factory;
    private readonly IOptionsMonitorCache<TOptions> _cache;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="factory"></param>
    /// <param name="cache"></param>
    public TenantOptions(IOptionsFactory<TOptions> factory, IOptionsMonitorCache<TOptions> cache)
    {
        _factory = factory;
        _cache = cache;
    }

    /// <summary>
    /// 
    /// </summary>
    public TOptions Value => Get(Options.DefaultName);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public TOptions Get(string name)
    {
        return _cache.GetOrAdd(name, () => _factory.Create(name));
    }
}
