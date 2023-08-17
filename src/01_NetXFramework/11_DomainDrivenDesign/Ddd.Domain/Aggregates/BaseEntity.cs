using System.ComponentModel.DataAnnotations.Schema;

namespace NetX.Ddd.Domain.Aggregates
{
    public abstract class BaseEntity<T> : Aggregate<T>
    {
        [NotMapped]
        new public int Version { get; protected set; }

        /// <summary>
        /// 获取未提交的事件
        /// </summary>
        /// <returns></returns>
        public override EventBase[] DequeueUncommittedEvents()
        {
            return base.DequeueUncommittedEvents();
        }

        /// <summary>
        /// add an eventbase object to the end of the queue
        /// </summary>
        /// <param name="event"></param>
        public void AddDomainEvent(EventBase @event)
        {
            base.Enqueue(@event);
        }
    }
}
