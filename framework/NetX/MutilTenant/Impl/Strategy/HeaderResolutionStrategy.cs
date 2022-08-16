﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MutilTenant;

/// <summary>
/// 请求头解析策略
/// </summary>
public class HeaderResolutionStrategy : ITenantResolutionStrategy
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// 请求头解析策略实例
    /// </summary>
    /// <param name="httpContextAccessor"></param>
    public HeaderResolutionStrategy(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }


    /// <summary>
    /// 从请求头获取租户唯一标识
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetTenantIdentifierAsync()
    {
        string indentifier = string.Empty;
        var header = _httpContextAccessor.HttpContext.Request.Headers;
        if (null != header && header.ContainsKey(NetXConst.C_TENANT_HTTPREQUESTHEADERKEY))
            indentifier = header[NetXConst.C_TENANT_HTTPREQUESTHEADERKEY];
        return await Task.FromResult(indentifier);
    }
}
