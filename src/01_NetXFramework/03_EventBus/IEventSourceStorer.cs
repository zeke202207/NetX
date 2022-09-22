namespace NetX.EventBus;

/// <summary>
/// 事件源存储器
/// 存储器用于事件溯源
/// </summary>
public interface IEventSourceStorer
{
    /// <summary>
    /// 事件源写入存储器
    /// </summary>
    /// <param name="eventSource"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask WriteAsync(IEventSource eventSource, CancellationToken cancellationToken);

    /// <summary>
    /// 从存储器中读取事件源
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask<IEventSource> ReadAsync(CancellationToken cancellationToken);
}
