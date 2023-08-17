namespace NetX.MemoryQueue
{
    /// <summary>
    /// 消息队列
    /// </summary>
    public interface IPublisher
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="T">消息类型</typeparam>
        /// <param name="queueName">队列名称</param>
        /// <param name="message">具体消息</param>
        /// <returns></returns>
        Task<bool> Publish<T>(string queueName, T message) where T : MessageArgument;
    }
}
