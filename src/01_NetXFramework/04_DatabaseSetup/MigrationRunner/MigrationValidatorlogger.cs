using FluentMigrator.Runner;
using Microsoft.Extensions.Logging;

namespace NetX.DatabaseSetup;

/// <summary>
/// 
/// </summary>
public class MigrationValidatorlogger : ILogger<MigrationValidator>
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <param name="state"></param>
    /// <returns></returns>
    public IDisposable BeginScope<TState>(TState state)
    {
#pragma warning disable CS8603 // 可能返回 null 引用。
        return null;
#pragma warning restore CS8603 // 可能返回 null 引用。
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logLevel"></param>
    /// <returns></returns>
    public bool IsEnabled(LogLevel logLevel)
    {
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    /// <param name="logLevel"></param>
    /// <param name="eventId"></param>
    /// <param name="state"></param>
    /// <param name="exception"></param>
    /// <param name="formatter"></param>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        //TODO:Write Log
    }
}
