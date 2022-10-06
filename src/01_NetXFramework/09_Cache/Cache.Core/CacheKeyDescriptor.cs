using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Cache.Core;

/// <summary>
/// 缓存键描述信息
/// </summary>
public class CacheKeyDescriptor
{
    /// <summary>
    /// 所属模块唯一标识
    /// </summary>
    public string ModuleId { get; set; }

    /// <summary>
    /// 所属模块编码
    /// </summary>
    public string ModuleName { get; set; }

    /// <summary>
    /// 名称KEY
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 说明
    /// </summary>
    public string Desc { get; set; }
}
