using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX;

/// <summary>
/// 模块类型
/// </summary>
public enum ModuleType :byte
{
    /// <summary>
    /// 系统框架模块
    /// </summary>
    FrameworkModule,
    /// <summary>
    /// 业务模块
    /// </summary>
    UserModule
}
