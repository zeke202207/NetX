using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Logging;

/// <summary>
/// 数据库日志记录器提供程序
/// </summary>
/// <remarks>https://docs.microsoft.com/zh-cn/dotnet/core/extensions/custom-logging-provider</remarks>
public sealed class NetXLoggerProvider : ILoggerProvider
{
    /// <summary>
    /// 存储多日志类的日志记录器
    /// </summary>
    private readonly ConcurrentDictionary<string, NetXLogger> _databaseLoggers = new();

    /// <summary>
    /// 数据库日志配置选项
    /// </summary>
    internal NetXLoggerOptions LoggerOptions { get; private set; }

    /// <summary>
    /// 日志消息队列（线程安全）
    /// </summary>
    private readonly BlockingCollection<LogMessage> _logMessageQueue = new(1024);

    /// <summary>
    /// 数据库日志写入器
    /// </summary>
    private ILoggingWriter _databaseLoggingWriter;

    /// <summary>
    /// 数据库日志写入器作用域范围
    /// </summary>
    internal IServiceScope _serviceScope;

    /// <summary>
    /// 长时间运行的后台任务
    /// </summary>
    /// <remarks>实现不间断写入</remarks>
    private Task _processQueueTask;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="databaseLoggerOptions"></param>
    public NetXLoggerProvider(NetXLoggerOptions databaseLoggerOptions)
    {
        LoggerOptions = databaseLoggerOptions;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public ILogger CreateLogger(string categoryName)
    {
        return _databaseLoggers.GetOrAdd(categoryName, name => new NetXLogger(name, this));
    }

    /// <summary>
    /// 释放托管资源
    /// </summary>
    public void Dispose()
    {
        // 标记日志消息队列停止写入
        _logMessageQueue.CompleteAdding();
        // 清空数据库日志记录器
        _databaseLoggers.Clear();
        // 释放数据库写入器作用域范围
        _serviceScope?.Dispose();
    }

    /// <summary>
    /// 将日志消息写入队列中等待后台任务出队写入数据库
    /// </summary>
    /// <param name="logMsg">结构化日志消息</param>
    internal void WriteToQueue(LogMessage logMsg)
    {
        // 只有队列可持续入队才写入
        if (!_logMessageQueue.IsAddingCompleted)
        {
            try
            {
                _logMessageQueue.Add(logMsg);
                return;
            }
            catch (InvalidOperationException) { }
        }
    }


    /// <summary>
    /// 设置服务提供器
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="databaseLoggingWriterType"></param>
    internal void SetServiceProvider(IServiceProvider serviceProvider, Type databaseLoggingWriterType)
    {
        // 解析服务作用域工厂服务
        var serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        // 创建服务作用域
        _serviceScope = serviceScopeFactory.CreateScope();
        // 基于当前作用域创建数据库日志写入器
        _databaseLoggingWriter = _serviceScope.ServiceProvider.GetRequiredService(databaseLoggingWriterType) as ILoggingWriter;
        // 创建长时间运行的后台任务，并将日志消息队列中数据写入存储中
        _processQueueTask = Task.Factory.StartNew(state => ((NetXLoggerProvider)state).ProcessQueue()
            , this, TaskCreationOptions.LongRunning);
    }

    /// <summary>
    /// 将日志消息写入数据库中
    /// </summary>
    private void ProcessQueue()
    {
        foreach (var logMsg in _logMessageQueue.GetConsumingEnumerable())
        {
            try
            {
                // 调用数据库写入器写入数据库方法
                _databaseLoggingWriter.Write(logMsg);
            }
            catch (Exception ex)
            {
                // 处理数据库写入错误
                if (LoggerOptions.HandleWriteError != null)
                {
                    var databaseWriteError = new NetXWriteError(ex);
                    LoggerOptions.HandleWriteError(databaseWriteError);
                }
                // 这里不抛出异常，避免中断日志写入
                else { }
            }
            finally
            {
                // 清空日志上下文
                ClearScopeContext(logMsg.LogName);
            }
        }
    }

    /// <summary>
    /// 清空日志上下文
    /// </summary>
    /// <param name="categoryName"></param>
    private void ClearScopeContext(string categoryName)
    {
        var isExist = _databaseLoggers.TryGetValue(categoryName, out var fileLogger);
        if (isExist)
        {
            //fileLogger.Context?.Properties?.Clear();
            fileLogger.Context = null;
        }
    }
}
