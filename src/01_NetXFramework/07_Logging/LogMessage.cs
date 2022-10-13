using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Logging;

/// <summary>
/// 日志实体类
/// </summary>
public struct LogMessage
{
    /// <summary>
    /// 日志对象实例
    /// </summary>
    /// <param name="logName">记录器类别名称</param>
    /// <param name="logLevel">日志级别</param>
    /// <param name="eventId">事件 Id</param>
    /// <param name="message">日志消息</param>
    /// <param name="exception">异常对象</param>
    /// <param name="context">日志上下文</param>
    /// <param name="state"></param>
    /// <param name="logDateTime">日志记录时间</param>
    /// <param name="threadId">线程id</param>
    public LogMessage(
        string logName,
        LogLevel logLevel,
        EventId eventId,
        string message,
        Exception exception,
        LogContext context,
        object state,
        DateTime logDateTime,
        int threadId
        )
    {
        LogName = logName;
        Message = message;
        LogLevel = logLevel;
        EventId = eventId;
        Exception = exception;
        Context = context;
        State = state;
        LogDateTime = logDateTime;
        ThreadId = threadId;
    }

    /// <summary>
    /// 记录器类别名称
    /// </summary>
    public readonly string LogName;

    /// <summary>
    /// 日志级别
    /// </summary>
    public readonly LogLevel LogLevel;

    /// <summary>
    /// 事件 Id
    /// </summary>
    public readonly EventId EventId;

    /// <summary>
    /// 日志消息
    /// </summary>
    public string Message { get; internal set; }

    /// <summary>
    /// 异常对象
    /// </summary>
    public readonly Exception Exception;

    /// <summary>
    /// 状态
    /// </summary>
    public readonly object State;

    /// <summary>
    /// 日志记录时间
    /// </summary>
    public readonly DateTime LogDateTime;

    /// <summary>
    /// 线程 Id
    /// </summary>
    public readonly int ThreadId;

    /// <summary>
    /// 日志上下文
    /// </summary>
    public LogContext Context;

    /// <summary>
    /// 重写默认输出
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return Message;
    }
}
