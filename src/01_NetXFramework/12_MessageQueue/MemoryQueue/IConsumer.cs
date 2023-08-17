namespace NetX.MemoryQueue
{
    /// <summary>
    /// 消费者接口
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public interface IConsumer<in TMessage> where TMessage : MessageArgument
    {
        string QueueName { get; }
        Task Handle(TMessage message);
    }
}
