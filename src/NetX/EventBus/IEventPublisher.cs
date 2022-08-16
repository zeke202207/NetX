namespace NetX.EventBus;

/// <summary>
/// 事件发布接口
/// </summary>
public interface IEventPublisher
{
    /// <summary>
    /// 发布一个事件源
    /// </summary>
    /// <param name="eventSource"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task PublishAsync(IEventSource eventSource, CancellationToken cancellationToken);
}
