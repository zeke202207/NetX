namespace NetX.EventBus;

/// <summary>
/// 事件订阅者特性标签
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class EventSubscribeAttribute : Attribute
{
    /// <summary>
    /// 事件唯一标识
    /// </summary>
    public string EventId { get; set; }

    /// <summary>
    /// 订阅者特性实例
    /// </summary>
    /// <param name="eventId">事件唯一标识,必须为guid</param>
    public EventSubscribeAttribute(string @eventId)
    {
        if (string.IsNullOrEmpty(eventId))
            throw new ArgumentException("string must not empty.");
        EventId = eventId;
    }
}
