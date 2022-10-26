namespace NetX.EventBus;

/// <summary>
/// 事件处理执行前上下文
/// </summary>
public sealed class EventHandlerExecutingContext : EventHandlerContext
{
    /// <summary>
    /// 事件执行上下文实例
    /// </summary>
    /// <param name="source">事件源</param>
    /// <param name="properties">共享上下文数据</param>
    public EventHandlerExecutingContext(IEventSource source, IDictionary<object, object> properties)
        : base(source, properties)
    {
    }

    /// <summary>
    /// 执行前时间
    /// </summary>
    public DateTime ExecutingTime { get; internal set; }
}
