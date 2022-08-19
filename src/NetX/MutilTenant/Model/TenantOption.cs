using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MutilTenant;

/// <summary>
/// 租户设置信息
/// </summary>
public class TenantOption
{
    /// <summary>
    /// 租户配置信息
    /// </summary>
    /// <param name="tenantType"></param>
    public TenantOption(TenantType tenantType)
    {
        TenantType = tenantType;
    }

    /// <summary>
    /// 应用程序租户类型
    /// </summary>
    public TenantType TenantType { get; private set; }

    /// <summary>
    /// 应用程序数据配置
    /// </summary>
    public DatabaseInfo DatabaseInfo { get; set; }
}
