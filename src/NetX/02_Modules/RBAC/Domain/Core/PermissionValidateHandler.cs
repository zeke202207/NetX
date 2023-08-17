using Microsoft.AspNetCore.Http;
using NetX.Authentication.Core;
using NetX.Common;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.Ddd.Core;
using NetX.EventBus;
using NetX.RBAC.Domain;
using NetX.RBAC.Models;
using NetX.Tenants;

namespace NetX.RBAC;

/// <summary>
///  后台权限验证器
/// </summary>
[Scoped]
public class PermissionValidateHandler : IPermissionValidateHandler
{
    /// <summary>
    /// 权限缓存
    /// </summary>
    private readonly IPermissionCache _cacheManager;
    private readonly IEventPublisher _publisher;
    private readonly IQueryBus _roleQuery;

    /// <summary>
    /// api权限验证 
    /// </summary>
    /// <param name="cacheManager">缓存实例</param>
    /// <param name="publisher">事件发布者实例</param>
    /// <param name="accountService">账号服务实例</param>
    public PermissionValidateHandler(
        IPermissionCache cacheManager,
        IEventPublisher publisher,
        IQueryBus roleQuery)
    {
        this._cacheManager = cacheManager;
        this._publisher = publisher;
        this._roleQuery = roleQuery;
    }

    /// <summary>
    /// 后台权限验证
    /// </summary>
    /// <param name="context">Encapsulates all HTTP-specific information about an individual HTTP request.</param>
    /// <param name="routeValues">路由</param>
    public async Task<bool> Validate(HttpContext context, IDictionary<string, string?> routeValues)
    {
        var userId = TenantContext.CurrentTenant.Principal?.UserId;
        var roleId = TenantContext.CurrentTenant.Principal?.RoleId;
        //如果没有userid和roleid信息，验证失败
        if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(roleId))
            return false;
        //根据userid获取是否需要api验证，获取api权限集合
        //1.判断缓存中是否存在key
        var cacheKey = roleId.ToRolePermissionCacheKey();
        //仅进行controller和action的模糊匹配
        //var router = context.Request.Path.ToString().ToLower();
        var router = new PermissionCacheApiModel($"{routeValues["controller"]}/{routeValues["action"]}", context.Request.Method);
        if (await _cacheManager.ExistsAsync(cacheKey))
            return await GetFromeCache(cacheKey, router);
        else
            return await GetFromeDababase(cacheKey, roleId, router);
    }

    /// <summary>
    /// 从缓存获取角色权限集合
    /// </summary>
    /// <param name="cacheKey">缓存key</param>
    /// <param name="router">路由</param>
    /// <returns></returns>
    private async Task<bool> GetFromeCache(string cacheKey, PermissionCacheApiModel apiCacheModel)
    {
        var model = await _cacheManager.GetAsync(cacheKey);
        if (null == model)
        {
            await _cacheManager.RemoveAsync(cacheKey);
            return false;
        }
        return Check(model, apiCacheModel);
    }

    /// <summary>
    /// 从数据库获取用户角色权限集合
    /// </summary>
    /// <param name="cacheKey">缓存key</param>
    /// <param name="userId">用户唯一标识</param>
    /// <param name="router">路由</param>
    /// <returns></returns>
    private async Task<bool> GetFromeDababase(string cacheKey, string roleId, PermissionCacheApiModel apiCacheModel)
    {
        //0.验证是否需要后台checkapi
        var roleModelResult = await _roleQuery.Send<RoleByIdQuery, ResultModel>(new RoleByIdQuery(roleId)) as ResultModel<RoleModel>;
        if (null == roleModelResult || roleModelResult.Code != Common.ResultEnum.SUCCESS)
            return false;
        //1.获取role信息，判定是否需要鉴权
        var cacheModel = new PermissionCacheModel()
        {
            RoleId = roleId,
            CheckApi = roleModelResult.Result.ApiCheck.ToBool(),
        };
        if (!cacheModel.CheckApi)
        {
            await _cacheManager.SetAsync(cacheKey, cacheModel);
            return true;
        }
        var result = await _roleQuery.Send<RoleApiQuery, ResultModel>(new RoleApiQuery(roleId));
        if (null == result || result.Code != Common.ResultEnum.SUCCESS)
            return false;
        var resultList = result as ResultModel<IEnumerable<ApiModel>>;
        cacheModel.ApiCaches.AddRange(resultList.Result.Select(p => new PermissionCacheApiModel(p.Path, p.Method)));
        //2.添加缓存
        await _cacheManager.SetAsync(cacheKey, cacheModel);
        //3.checked
        return Check(cacheModel, apiCacheModel);
    }

    /// <summary>
    /// 校验
    /// </summary>
    /// <param name="permission">权限实体</param>
    /// <param name="router">路由</param>
    /// <returns></returns>
    private bool Check(PermissionCacheModel permission, PermissionCacheApiModel apiModel)
    {
        if (!permission.CheckApi)
            return true;
        //TODO：性能优化
        return permission.ApiCaches.Contains(apiModel, new ApiEquelCompare());
    }
}