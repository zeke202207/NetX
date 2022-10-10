using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Logging;

/// <summary>
/// <see cref="ILogger"/> 拓展
/// </summary>
public static class ILoggerExtensions
{
    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="properties">建议使用 ConcurrentDictionary 类型</param>
    /// <returns></returns>
    public static ILogger ScopeContext(this ILogger logger, IDictionary<object, object> properties)
    {
        if (properties == null) return logger;
        logger.BeginScope(new LogContext { Properties = properties });

        return logger;
    }

    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configure"></param>
    /// <returns></returns>
    public static ILogger ScopeContext(this ILogger logger, Action<LogContext> configure)
    {
        var logContext = new LogContext();
        configure?.Invoke(logContext);

        logger.BeginScope(logContext);

        return logger;
    }

    /// <summary>
    /// 配置日志上下文
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static ILogger ScopeContext(this ILogger logger, LogContext context)
    {
        if (context == null) return logger;
        logger.BeginScope(context);

        return logger;
    }
}
