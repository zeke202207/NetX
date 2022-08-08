namespace NetX.EventBus;

/// <summary>
/// 事件处理程序包装器
/// 用于主机服务启动时将所有处理程序和事件id进行包装绑定
/// </summary>
internal sealed class EventHandlerWrapper
{
    /// <summary>
    /// 事件Id
    /// </summary>
    internal string EventId { get; set; }

    /// <summary>
    /// 处理程序
    /// </summary>
    internal Func<EventHandlerExecutingContext, Task> Handler;

    internal EventHandlerWrapper(string @eventId)
    {
        EventId = @eventId;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="eventId"></param>
    /// <returns></returns>
    internal bool ShouldRun(string @eventId)
    {
        return EventId == @eventId;
    }
}
