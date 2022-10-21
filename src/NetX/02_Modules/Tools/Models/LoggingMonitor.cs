using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.Models
{
    internal class LoggingMonitor
    {
        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { get; set; }

        /// <summary>
        /// 接口名称
        /// </summary>
        public string ActionName { get; set; }

        /// <summary>
        /// 客户端地址
        /// </summary>
        public string RemoteIPv4 { get; set; }

        /// <summary>
        /// 请求方法类型
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// 接口耗时
        /// </summary>
        public long TimeOperationElapsedMilliseconds { get; set; }

        /// <summary>
        /// 授权票据集合
        /// </summary>
        public List<LoggingMonitorClaims> AuthorizationClaims { get; set; }
    }

    internal class LoggingMonitorClaims
    {
        /// <summary>
        /// 票据类型
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// 票据值
        /// </summary>
        public string Value { get; set; }
    }
}
