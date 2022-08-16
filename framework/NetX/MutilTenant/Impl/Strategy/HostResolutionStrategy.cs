using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MutilTenant;

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
    /// </summary>
    /// <returns>租户身份标识</returns>
    public async Task<string> GetTenantIdentifierAsync()
    {
        return await Task.FromResult(_httpContextAccessor.HttpContext.Request.Host.Host);
    }
}
