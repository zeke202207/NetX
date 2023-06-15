using Microsoft.AspNetCore.Http;

namespace NetX.Tenants;

/// <summary>
/// 根据主机解析租户身份
/// </summary>
public class HostResolutionStrategy : ITenantResolutionStrategy
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// 主机解析策略
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public HostResolutionStrategy(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 解析租户身份 
    /// 二级域名作为唯一标识
    /// </summary>
    /// <returns>租户身份标识</returns>
    public async Task<string> GetTenantIdentifierAsync()
    {
        if (null == _httpContextAccessor.HttpContext)
            return await Task.FromResult(string.Empty);
        var host = _httpContextAccessor.HttpContext.Request.Host.Host;
        return host.Substring(0, host.IndexOf("."));
    }
}
