using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Logging;

/// <summary>
/// 数据库写入错误信息上下文
/// </summary>
public sealed class NetXWriteError
{
    internal NetXWriteError(Exception exception)
    {
        Exception = exception;
    }

    /// <summary>
    /// 引发数据库写入异常信息
    /// </summary>
    public Exception Exception { get; private set; }
}
