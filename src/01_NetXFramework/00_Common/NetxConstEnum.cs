using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Common;

/// <summary>
/// 结果状态枚举
/// </summary>
public enum ResultEnum
{
    /// <summary>
    /// 成功
    /// </summary>
    SUCCESS = 0,
    /// <summary>
    /// 失败
    /// </summary>
    ERROR = -1,
    /// <summary>
    /// 超时
    /// </summary>
    TIMEOUT = 401,
}
