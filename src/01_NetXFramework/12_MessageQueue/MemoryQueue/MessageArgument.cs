using NetX.Common;

namespace NetX.MemoryQueue
{
    /// <summary>
    /// 消息
    /// </summary>
    public abstract class MessageArgument : MessageGroupArgument
    {
        public Guid Id { get; }

        /// <summary>
        /// 时间戳(UTC 1970-01-01 00:00:00)
        /// </summary>
        public long Timestamp { get; set; }

        protected MessageArgument()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow.ToTimeStamp();
        }
    }

    /// <summary>
    /// 消息分组聚合根
    /// 消息体包含此消息分组信息
    /// 分组为空：消息存放队列为发布的队列名称
    /// 分组不为空：消息存放的队列为 发布的队列名称 + 分组名称
    /// 既，不同分组的相同队列名称的数据，将存放与不同的队列
    /// </summary>
    public abstract class MessageGroupArgument
    {
        public string GroupName { get; set; }
    }
}
