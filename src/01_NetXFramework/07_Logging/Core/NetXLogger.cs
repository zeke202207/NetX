using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Logging;

/// <summary>
/// 数据库日志记录器
/// </summary>
/// <remarks>https://docs.microsoft.com/zh-cn/dotnet/core/extensions/custom-logging-provider</remarks>
public sealed class NetXLogger : ILogger
{

    /// <summary>
    /// 排除日志分类名
    /// </summary>
    /// <remarks>避免数据库日志死循环</remarks>
    private static readonly string[] ExcludesOfLogCategoryName = new string[]
    {
        "Microsoft.EntityFrameworkCore"
    };

    /// <summary>
    /// 记录器类别名称
    /// </summary>
    private readonly string _logName;

    /// <summary>
    /// 数据库记录器提供器
    /// </summary>
    private readonly NetXLoggerProvider _databaseLoggerProvider;

    /// <summary>
    /// 日志配置选项
    /// </summary>
    private readonly NetXLoggerOptions _options;

    /// <summary>
    /// 日志上下文
    /// </summary>
    public LogContext Context { get; internal set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="logName">记录器类别名称</param>
    /// <param name="databaseLoggerProvider">数据库记录器提供器</param>
    public NetXLogger(string logName, NetXLoggerProvider databaseLoggerProvider)
    {
        _logName = logName;
        _databaseLoggerProvider = databaseLoggerProvider;
        _options = databaseLoggerProvider.LoggerOptions;
    }

    /// <summary>
    /// 开始逻辑操作范围
    /// </summary>
    /// <typeparam name="TState">标识符类型参数</typeparam>
    /// <param name="state">要写入的项/对象</param>
    /// <returns><see cref="IDisposable"/></returns>
    public IDisposable BeginScope<TState>(TState state)
    {
        // 设置日志上下文
        if (state is LogContext context)
        {
            if (Context == null) Context = new LogContext().SetRange(context.Properties);
            else Context.SetRange(context.Properties);
        }

        return default;
    }

    /// <summary>
    /// 检查是否已启用给定日志级别
    /// </summary>
    /// <param name="logLevel">日志级别</param>
    /// <returns><see cref="bool"/></returns>
    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel >= _options.MinimumLevel;
    }

    /// <summary>
    /// 写入日志项
    /// </summary>
    /// <typeparam name="TState">标识符类型参数</typeparam>
    /// <param name="logLevel">日志级别</param>
    /// <param name="eventId">事件 Id</param>
    /// <param name="state">要写入的项/对象</param>
    /// <param name="exception">异常对象</param>
    /// <param name="formatter">日志格式化器</param>
    /// <exception cref="ArgumentNullException"></exception>
    public void Log<TState>(
        LogLevel logLevel, 
        EventId eventId, 
        TState state, 
        Exception? exception, 
        Func<TState, Exception?, string> formatter)
    {
        // 判断日志级别是否有效
        if (!IsEnabled(logLevel)) 
            return;
        // 排除数据库自身日志
        if (ExcludesOfLogCategoryName.Any(u => _logName.StartsWith(u))) 
            return;
        // 检查日志格式化器
        if (formatter == null) 
            throw new ArgumentNullException(nameof(formatter));
        // 获取格式化后的消息
        var message = formatter(state, exception);
        var logDateTime = _options.UseUtcTimestamp ? DateTime.UtcNow : DateTime.Now;
        bool isAudit = false;
        bool isLogin = false;
        if (null != Context)
        {
            var value = Context.Get(LoggingConst.C_LOGGING_AUDIT);
            bool.TryParse(value?.ToString(), out isAudit);
            var login = Context.Get(LoggingConst.C_LOGGING_LOGIN);
            bool.TryParse(login?.ToString(), out isLogin);
        }
        var logMsg = new LogMessage(
            _logName,
            logLevel,
            eventId,
            message,
            exception,
            Context,
            state,
            logDateTime,
            Environment.CurrentManagedThreadId,
            isAudit,
            isLogin);
        // 判断是否自定义了日志筛选器，如果是则检查是否符合条件
        if (_options.WriteFilter?.Invoke(logMsg) == false) 
            return;
        // 设置日志消息模板
        logMsg.Message = _options.MessageFormat != null
            ? _options.MessageFormat(logMsg)
            : Penetrates.OutputStandardMessage(logMsg, _options.DateFormat);
        // 空检查
        if (logMsg.Message is null) 
            return;
        // 写入日志队列
        _databaseLoggerProvider.WriteToQueue(logMsg);
    }
}
