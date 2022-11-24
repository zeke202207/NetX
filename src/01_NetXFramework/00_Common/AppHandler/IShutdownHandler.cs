using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Common;

/// <summary>
/// 应用程序关闭处理接口
/// </summary>
public interface IShutdownHandler
{
    /// <summary>
    /// 处理操作
    /// </summary>
    Task Handle();
}
