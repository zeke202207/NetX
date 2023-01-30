using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Logging;

/// <summary>
/// 日志写入器
/// </summary>
public interface ILoggingWriter
{
    /// <summary>
    /// 写入日志
    /// </summary>
    /// <param name="message"></param>
    void Write(LogMessage message);
}
