using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Common;

/// <summary>
/// 全局访问定位器
/// </summary>
public static class ServiceLocator
{
    /// <summary>
    /// 注入服务定位器
    /// </summary>
    public static IServiceProvider? Instance { get; set; }
}
