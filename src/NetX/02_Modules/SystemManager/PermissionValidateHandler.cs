using Microsoft.AspNetCore.Http;
using NetX.Authentication.Core;
using NetX.Common.Attributes;

namespace NetX.SystemManager;

/// <summary>
///  后台权限验证器
/// </summary>
[Scoped]
public class PermissionValidateHandler : IPermissionValidateHandler
{
    /// <summary>
    /// 后台权限验证
    /// </summary>
    public bool Validate(HttpContext context, IDictionary<string, string> routeValues)
    {
        return true;
    }
}
