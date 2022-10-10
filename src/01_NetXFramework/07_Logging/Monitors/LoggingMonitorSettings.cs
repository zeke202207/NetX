using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace NetX.Logging.Monitors
{
    /// <summary>
    /// 日志监视器配置
    /// </summary>
    public sealed class LoggingMonitorSettings
    {
        /// <summary>
        /// 是否记录返回值
        /// </summary>
        /// <remarks>bool 类型，默认输出</remarks>
        public bool WithReturnValue { get; set; } = true;

        /// <summary>
        /// 设置返回值阈值
        /// </summary>
        /// <remarks>配置返回值字符串阈值，超过这个阈值将截断，默认全量输出</remarks>
        public int ReturnValueThreshold { get; set; } = 0;

        /// <summary>
        /// 配置 Json 输出行为
        /// </summary>
        public JsonBehavior JsonBehavior { get; set; } = JsonBehavior.None;

        /// <summary>
        /// 添加日志更多配置
        /// </summary>
        internal static Action<ILogger, LogContext, ActionExecutedContext> Configure { get; private set; }

        /// <summary>
        /// 配置日志更多功能
        /// </summary>
        /// <param name="configure"></param>
        public void ConfigureLogger(Action<ILogger, LogContext, ActionExecutedContext> configure)
        {
            Configure = configure;
        }

        /// <summary>
        /// 配置 Json 写入选项
        /// </summary>
        public JsonWriterOptions JsonWriterOptions { get; set; } = new JsonWriterOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            SkipValidation = true
        };
    }
}
