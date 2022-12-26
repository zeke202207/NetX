namespace Netx.Ddd.Domain;

public interface IAggregate : IAggregate<Guid>
{
}

public interface IAggregate<out T>
{
    T Id { get; }
    int Version { get; }

    EventBase[] DequeueUncommittedEvents();
}
