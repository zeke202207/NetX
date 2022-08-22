using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.App.AppHandler
{
    /// <summary>
    /// 应用程序关闭处理接口
    /// </summary>
    public interface IAppShutdownHandler
    {
        /// <summary>
        /// 处理操作
        /// </summary>
        Task Handle();
    }
}
