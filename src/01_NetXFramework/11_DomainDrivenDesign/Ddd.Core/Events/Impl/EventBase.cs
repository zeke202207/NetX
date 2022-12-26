using System.ComponentModel.DataAnnotations.Schema;

namespace Netx.Ddd.Core;

/// <summary>
/// Defines a publish interface
/// </summary>
public abstract class EventBase : INotification
{
    public string MessageType { get; protected set; }

    public Guid AggregateId { get; protected set; }

    [NotMapped]
    public DateTime Timestamp { get; private set; }

    public EventBase(Guid aggregateid)
    {
        MessageType = GetType().Name;
        Timestamp = DateTime.UtcNow;
        this.AggregateId= aggregateid;
    }
}
