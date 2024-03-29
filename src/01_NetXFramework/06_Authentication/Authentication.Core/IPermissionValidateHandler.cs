﻿using Microsoft.AspNetCore.Http;

namespace NetX.Authentication.Core;

/// <summary>
/// 权限验证接口
/// </summary>
public interface IPermissionValidateHandler
{
    /// <summary>
    /// 验证
    /// </summary>
    /// <returns></returns>
    Task<bool> Validate(HttpContext context, IDictionary<string, string?> routeValues);
}
