using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MutilTenant;

/// <summary>
/// 租户信息
/// </summary>
public class Tenant
{
    /// <summary>
    /// 租户Id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// 组合身份
    /// 在解析策略中使用此标识符进行解析
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// The Tenant Items
    /// 为开发人员预留扩展
    /// </summary>
    public Dictionary<string, object> Items { get; set; } = new Dictionary<string, object>();
}
