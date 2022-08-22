using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Authentication;

/// <summary>
/// 通用权限模块，登录即可访问，不需要授权
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class NoPermissionAttribute : AllowAnonymousAttribute
{
}
