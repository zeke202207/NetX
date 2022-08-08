namespace NetX.EventBus;

/// <summary>
/// 基于内存通道的事件发布者
/// </summary>
internal sealed class EventPublisher : IEventPublisher
{
    /// <summary>
    /// 事件源存储器
    /// </summary>
    private readonly IEventSourceStorer _eventSourceStorer;

    /// <summary>
    /// 时间发布者实例
    /// </summary>
    /// <param name="eventSourceStorer"></param>
    public EventPublisher(IEventSourceStorer eventSourceStorer)
    {
        _eventSourceStorer = eventSourceStorer;
    }

    /// <summary>
    /// 发布一条消息
    /// </summary>
    /// <param name="eventSource"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task PublishAsync(IEventSource eventSource, CancellationToken cancellationToken = default)
    {
        await _eventSourceStorer.WriteAsync(eventSource, cancellationToken);
    }
}
