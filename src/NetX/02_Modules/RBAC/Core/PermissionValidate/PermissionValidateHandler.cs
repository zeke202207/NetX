using Microsoft.AspNetCore.Http;
using NetX.Authentication.Core;
using NetX.Cache.Core;
using NetX.Common.Attributes;
using NetX.EventBus;
using NetX.RBAC.Core;
using NetX.RBAC.Models;
using NetX.Tenants;
using System.Diagnostics.CodeAnalysis;

namespace NetX.RBAC.Core;

/// <summary>
///  后台权限验证器
/// </summary>
[Scoped]
public class PermissionValidateHandler : IPermissionValidateHandler
{
    /// <summary>
    /// 权限缓存
    /// </summary>
    private readonly ICacheProvider _cache;
    private readonly IEventPublisher _publisher;
    private IAccountService _accountService;

    /// <summary>
    /// api权限验证 
    /// </summary>
    /// <param name="cache">缓存实例</param>
    /// <param name="publisher">事件发布者实例</param>
    /// <param name="accountService">账号服务实例</param>
    public PermissionValidateHandler(
        ICacheProvider cache,
        IEventPublisher publisher,
        IAccountService accountService)
    {
        this._cache = cache;
        this._accountService = accountService;
        this._publisher = publisher;
    }

    /// <summary>
    /// 后台权限验证
    /// </summary>
    /// <param name="context">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
    /// <param name="routeValues">路由</param>
    public async Task<bool> Validate(HttpContext context, IDictionary<string, string> routeValues)
    {
        var userId = TenantContext.CurrentTenant.Principal?.UserId;
        var roleId = TenantContext.CurrentTenant.Principal?.RoleId;
        //根据userid获取是否需要api验证，获取api权限集合
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(roleId))
            return false;
        //1.判断缓存中是否存在key
        var cacheKey = $"{CacheKeys.ACCOUNT_PERMISSIONS}{roleId}";
        var router = $"{routeValues["controller"]}/{routeValues["action"]}";
        if (await _cache.ExistsAsync(cacheKey))
            return await GetFromeCache(cacheKey, router);
        else
            return await GetFromeDababase(cacheKey, userId, router);
    }

    /// <summary>
    /// 从缓存获取角色权限集合
    /// </summary>
    /// <param name="cacheKey">缓存key</param>
    /// <param name="router">路由</param>
    /// <returns></returns>
    private async Task<bool> GetFromeCache(string cacheKey, string router)
    {
        var model = await _cache.GetAsync<ApiPermissionModel>(cacheKey);
        if (null == model)
            return false;
        return Check(model, router);
    }

    /// <summary>
    /// 从数据库获取用户角色权限集合
    /// </summary>
    /// <param name="cacheKey">缓存key</param>
    /// <param name="userId">用户唯一标识</param>
    /// <param name="router">路由</param>
    /// <returns></returns>
    private async Task<bool> GetFromeDababase(string cacheKey, string userId, string router)
    {
        var result = await _accountService.GetApiPermCode(userId);
        if (result.Code != Common.Models.ResultEnum.SUCCESS)
            return false;
        await PublishCache(cacheKey, result.Result);
        return Check(result.Result, router);
    }

    /// <summary>
    /// 缓存权限
    /// </summary>
    /// <param name="cacheKey">缓存key</param>
    /// <param name="model">缓存value</param>
    /// <returns></returns>
    private async Task PublishCache(string cacheKey, ApiPermissionModel model)
    {
        var playload = new PermissionPayload()
        {
            CacheKey = cacheKey,
            OperationType = CacheOperationType.Set
        };
        playload.CacheModel.CheckApi = model.CheckApi;
        playload.CacheModel.Apis.AddRange(model.Apis);
        await _publisher.PublishAsync(new EventSource(RBACConst.C_RBAC_EVENT_KEY, playload), CancellationToken.None);
    }

    /// <summary>
    /// 校验
    /// </summary>
    /// <param name="permission">权限实体</param>
    /// <param name="router">路由</param>
    /// <returns></returns>
    private bool Check(ApiPermissionModel permission, string router)
    {
        if (!permission.CheckApi)
            return true;
        return permission.Apis.Contains($"{router}", new ApiEquelCompare());
    }
}