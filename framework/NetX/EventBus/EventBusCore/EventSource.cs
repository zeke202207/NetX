namespace NetX.EventBus;

/// <summary>
/// 内存通道的事件源
/// </summary>
public sealed class EventSource : IEventSource
{
    /// <summary>
    /// 事件源对象
    /// </summary>
    /// <param name="eventId"></param>
    public EventSource(string @eventId)
    {
        EventId = @eventId;
    }

    /// <summary>
    /// 事件源对象
    /// </summary>
    /// <param name="eventId">事件Id</param>
    /// <param name="payload">事件承载数据</param>
    public EventSource(string eventId, object payload)
        : this(eventId)
    {
        Payload = payload;
    }

    /// <summary>
    /// 事件ID
    /// </summary>
    public string EventId { get; }

    /// <summary>
    /// 事件承载数据
    /// </summary>
    public object Payload { get; }

    /// <summary>
    /// 事件创建时间
    /// </summary>
    public DateTime CreatAt { get; } = DateTime.UtcNow;
}
