using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MutilTenant;

/// <summary>
/// 租户访问器 
/// </summary>
public interface ITenantAccessor<T> where T : Tenant
{
    /// <summary>
    /// 租户信息
    /// </summary>
    T Tenant { get; }
}
