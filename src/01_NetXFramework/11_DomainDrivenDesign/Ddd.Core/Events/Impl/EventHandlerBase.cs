namespace Netx.Ddd.Core;

/// <summary>
/// Defines a Subscribe interface
/// </summary>
public abstract class EventHandlerBase<TEvent> : INotificationHandler<EventBase>
{
    public abstract Task Handle(EventBase notification, CancellationToken cancellationToken);
}
