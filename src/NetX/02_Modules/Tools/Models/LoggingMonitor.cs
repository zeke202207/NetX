using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.Models
{
    internal class LoggingMonitor
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string RemoteIPv4 { get; set; }
        public string HttpMethod { get; set; }
        /// <summary>
        /// 接口耗时
        /// </summary>
        public long TimeOperationElapsedMilliseconds { get; set; }
        public List<LoggingMonitorClaims> AuthorizationClaims { get; set; }
    }

    internal class LoggingMonitorClaims
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
