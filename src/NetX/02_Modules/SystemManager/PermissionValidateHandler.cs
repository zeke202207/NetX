using Microsoft.AspNetCore.Http;
using NetX.Authentication.Core;
using NetX.Cache.Core;
using NetX.Common.Attributes;
using NetX.SystemManager.Core;
using NetX.Tenants;
using System.Diagnostics.CodeAnalysis;

namespace NetX.SystemManager;

/// <summary>
///  后台权限验证器
/// </summary>
[Scoped]
public class PermissionValidateHandler : IPermissionValidateHandler
{
    /// <summary>
    /// 权限缓存
    /// 目前暂时不用缓存
    /// 如果性能出现问题，考虑缓存方式
    /// </summary>
    private readonly ICacheProvider _cache;
    private IAccountService _accountService;

    /// <summary>
    /// api权限验证 
    /// </summary>
    /// <param name="cache"></param>
    /// <param name="accountService"></param>
    public PermissionValidateHandler(ICacheProvider cache , IAccountService accountService)
    {
        this._cache = cache;
        this._accountService = accountService;
    }

    /// <summary>
    /// 后台权限验证
    /// </summary>
    public async Task<bool> Validate(HttpContext context, IDictionary<string, string> routeValues)
    {
        var userId = TenantContext.CurrentTenant.Principal?.UserId;
        //根据userid获取是否需要api验证，获取api权限集合
        if (string.IsNullOrEmpty(userId))
            return false;
        var result = await _accountService.GetApiPermCode(userId);
        if (result.Code != Common.Models.ResultEnum.SUCCESS)
            return false;
        if (!result.Result.CheckApi)
            return true;
        return result.Result.Apis.Contains($"{routeValues["controller"]}/{routeValues["action"]}", new ApiEquelCompare());
    }
}