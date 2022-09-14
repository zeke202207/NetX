using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.App;

/// <summary>
/// 应用程序启动处理接口
/// </summary>
public interface IAppStartHandler
{
    /// <summary>
    /// 
    /// </summary>
    Task Handle();
}
