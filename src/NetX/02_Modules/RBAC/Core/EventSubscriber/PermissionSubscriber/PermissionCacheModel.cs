using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Core;

/// <summary>
/// 权限校验缓存实体
/// </summary>
public class PermissionCacheModel
{
    /// <summary>
    /// 是否进行pai验证
    /// </summary>
    public bool CheckApi { get; set; } = false;

    /// <summary>
    /// api权限列表集合
    /// </summary>
    public List<string> Apis { get; set; } = new List<string>();
}
