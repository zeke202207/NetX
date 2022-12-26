namespace Netx.Ddd.Domain;

public abstract class Aggregate : Aggregate<Guid>, IAggregate
{

}

public abstract class Aggregate<T> : IAggregate<T> where T : notnull
{
    public T Id { get; set; } = default!;

    public int Version { get; protected set; }

    [NonSerialized]
    private readonly Queue<EventBase> _uncommittedEvents = new();

    /// <summary>
    /// 获取未提交的事件
    /// </summary>
    /// <returns></returns>
    public virtual EventBase[] DequeueUncommittedEvents()
    {
        var dequeuedEvents = _uncommittedEvents.ToArray();
        _uncommittedEvents.Clear();
        return dequeuedEvents;
    }

    /// <summary>
    /// add an eventbase object to the end of the queue
    /// </summary>
    /// <param name="event"></param>
    protected virtual void Enqueue(EventBase @event)
    {
        _uncommittedEvents.Enqueue(@event);
    }
}

