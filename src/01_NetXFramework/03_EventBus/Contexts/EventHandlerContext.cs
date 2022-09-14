namespace NetX.EventBus;

/// <summary>
/// 事件处理上下文
/// </summary>
public abstract class EventHandlerContext
{
    /// <summary>
    /// 事件源
    /// </summary>
    public IEventSource Source { get; }

    /// <summary>
    /// 共享上下文数据
    /// </summary>
    public IDictionary<object, object> Properties { get; }

    /// <summary>
    /// 事件处理上下文实例
    /// </summary>
    /// <param name="source">事件源</param>
    /// <param name="properties">共享上下文数据</param>
    public EventHandlerContext(IEventSource source, IDictionary<object, object> properties)
    {
        Source = source;
        Properties = properties;
    }
}
