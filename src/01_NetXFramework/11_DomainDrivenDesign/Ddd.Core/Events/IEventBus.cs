namespace Netx.Ddd.Core;

public interface IEventBus
{
    Task PublishAsync(params EventBase[] events);
}
