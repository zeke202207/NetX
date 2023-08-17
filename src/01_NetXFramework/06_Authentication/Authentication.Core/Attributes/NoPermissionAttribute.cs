using Microsoft.AspNetCore.Authorization;

namespace NetX.Authentication.Core;

/// <summary>
/// 通用权限模块，登录即可访问，不需要授权
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class NoPermissionAttribute : AllowAnonymousAttribute
{
}
