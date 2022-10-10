using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Logging;

/// <summary>
/// 常量、公共方法配置类
/// </summary>
internal static class Penetrates
{
    /// <summary>
    /// 输出标准日志消息
    /// </summary>
    /// <param name="logMsg"></param>
    /// <param name="dateFormat"></param>
    /// <param name="disableColors"></param>
    /// <param name="isConsole"></param>
    /// <returns></returns>
    internal static string OutputStandardMessage(LogMessage logMsg
        , string dateFormat = "o")
    {
        // 空检查
        if (logMsg.Message is null) 
            return null;
        // 创建默认日志格式化模板
        var formatString = new StringBuilder();
        // 获取日志级别对应控制台的颜色
        formatString.Append(GetLogLevelString(logMsg.LogLevel));
        formatString.Append(": ");
        formatString.Append(logMsg.LogDateTime.ToString(dateFormat));
        formatString.Append(' ');
        formatString.Append(logMsg.LogName);
        formatString.Append('[');
        formatString.Append(logMsg.EventId.Id);
        formatString.Append(']');
        formatString.Append(' ');
        formatString.Append($"#{logMsg.ThreadId}");
        formatString.AppendLine();
        // 对日志内容进行缩进对齐处理
        formatString.Append(PadLeftAlign(logMsg.Message));
        // 如果包含异常信息，则创建新一行写入
        if (logMsg.Exception != null)
        {
            var exceptionMessage = $"{Environment.NewLine}{LoggingConst.C_LOGGING_EXCEPTION_SEPARATOR}{Environment.NewLine}{logMsg.Exception}{Environment.NewLine}{LoggingConst.C_LOGGING_EXCEPTION_SEPARATOR}";
            formatString.Append(PadLeftAlign(exceptionMessage));
        }
        // 返回日志消息模板
        return formatString.ToString();
    }

    /// <summary>
    /// 将日志内容进行对齐
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    private static string PadLeftAlign(string message)
    {
        var newMessage = string.Join(Environment.NewLine, message.Split(Environment.NewLine)
                    .Select(line => string.Empty.PadLeft(6, ' ') + line));

        return newMessage;
    }

    /// <summary>
    /// 获取日志级别短名称
    /// </summary>
    /// <param name="logLevel">日志级别</param>
    /// <returns></returns>
    internal static string GetLogLevelString(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => "trce",
            LogLevel.Debug => "dbug",
            LogLevel.Information => "info",
            LogLevel.Warning => "warn",
            LogLevel.Error => "fail",
            LogLevel.Critical => "crit",
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel)),
        };
    }
}
