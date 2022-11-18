using Microsoft.Extensions.DependencyInjection;
using NetX.Cache.Core;
using NetX.Common;
using NetX.Common.Attributes;
using NetX.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Core;

/// <summary>
/// 权限校验缓存事件处理程序
/// </summary>
[Scoped]
public class PermissionCacheEventHandler : IEventSubscriber
{
    /// <summary>
    /// 权限缓存
    /// </summary>
    private readonly ICacheProvider? _cache;

    /// <summary>
    /// 权限验证缓存实例
    /// </summary>
    public PermissionCacheEventHandler()
    {
        this._cache = ServiceLocator.Instance?.GetService<ICacheProvider>();
    }

    /// <summary>
    /// 处理缓存
    /// </summary>
    /// <param name="context">
    /// 事件上下文
    /// <code>
    ///     var playload = new PermissionPayload()
    ///     {
    ///         CacheKey = cacheKey,
    ///         OperationType = CacheOperationType.Set
    ///     };
    ///     playload.CacheModel.CheckApi = model.CheckApi;
    ///     playload.CacheModel.Apis.AddRange(model.Apis);
    ///     await _publisher.PublishAsync(new EventSource(RBACConst.C_RBAC_EVENT_KEY, playload), CancellationToken.None);
    /// </code>
    /// </param>
    /// <returns></returns>
    [EventSubscribe(RBACConst.C_RBAC_EVENT_KEY)]
    public async Task Handler(EventHandlerExecutingContext context)
    {
        if (null == context || null == context.Source.Payload || null == _cache)
            return;
        var playload = context.Source.Payload as PermissionPayload;
        if (null == playload || null == playload.CacheModel)
            return;
        if (playload.OperationType == CacheOperationType.Remove && await _cache.ExistsAsync(playload.CacheKey))
            await _cache.RemoveAsync(playload.CacheKey);
        else
            await _cache.SetAsync(playload.CacheKey, playload.CacheModel);
    }
}
