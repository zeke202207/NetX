namespace NetX.EventBus;

/// <summary>
/// 事件源（事件的承载对象）
/// </summary>
public interface IEventSource
{
    /// <summary>
    /// 事件的唯一标识
    /// </summary>
    string EventId { get; }

    /// <summary>
    /// 事件携带的数据
    /// </summary>
    object Payload { get; }

    /// <summary>
    /// 事件的创建时间
    /// </summary>
    DateTime CreatAt { get; }
}
