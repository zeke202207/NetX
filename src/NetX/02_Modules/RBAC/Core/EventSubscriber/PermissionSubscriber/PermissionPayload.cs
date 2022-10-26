using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Core;

/// <summary>
/// 缓存消息载体
/// </summary>
public class PermissionPayload
{
    /// <summary>
    /// 缓存操作类型
    /// </summary>
    public CacheOperationType OperationType { get; set; }

    /// <summary>
    /// 缓存key
    /// </summary>
    public string CacheKey { get; set; }

    /// <summary>
    /// 缓存内容
    /// </summary>
    public PermissionCacheModel CacheModel { get; set; } = new PermissionCacheModel();
}
