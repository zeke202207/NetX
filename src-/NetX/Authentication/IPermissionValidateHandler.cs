﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Authentication;

/// <summary>
/// 权限验证接口
/// </summary>
public interface IPermissionValidateHandler
{
    /// <summary>
    /// 验证
    /// </summary>
    /// <returns></returns>
    bool Validate(HttpContext context, IDictionary<string, string> routeValues);
}