namespace NetX.MemoryQueue
{
    /// <summary>
    /// 消息实际处理者
    /// </summary>
    internal abstract class MessageQueueHandler
    {
        public abstract bool IsBindReceivedEvent { get; }

        public abstract void BindReceivedEvent(Action<MessageArgument> received);

        /// <summary>
        /// 返回一个值，该值指示当前消息发布是否成功
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public abstract Task<bool> Publish(MessageArgument message);
    }
}
