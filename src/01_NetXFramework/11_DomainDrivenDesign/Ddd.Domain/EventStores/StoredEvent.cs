using System.ComponentModel.DataAnnotations.Schema;

namespace NetX.Ddd.Domain;

[Table("sys_eventstore")]
public class StoredEvent : EventBase
{
    public StoredEvent(EventBase theEvent, string data, string userid)
        : base(theEvent.AggregateId)
    {
        Id = Guid.NewGuid().ToString();
        AggregateId = theEvent.AggregateId;
        MessageType = theEvent.MessageType;
        Data = data;
        UserId = userid;
        CreateTime = base.Timestamp;
    }

    protected StoredEvent()
    : base(Guid.NewGuid()) { }

    public string Id { get; private set; }

    public string Data { get; private set; }

    public string UserId { get; private set; }

    public DateTime CreateTime { get; private set; }
}
