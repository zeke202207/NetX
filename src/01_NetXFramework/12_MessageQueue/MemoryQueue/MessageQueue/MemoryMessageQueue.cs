using System.Collections.Concurrent;

namespace NetX.MemoryQueue
{
    /// <summary>
    /// 基于内存的消息队列
    /// </summary>
    public class MemoryMessageQueue : IPublisher
    {
        private ConcurrentDictionary<string, MessageQueueHandler> pairs;
        private readonly ServiceFactory _serviceFactory;

        public MemoryMessageQueue(ServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            pairs = new ConcurrentDictionary<string, MessageQueueHandler>();
        }

        public async Task<bool> Publish<TMessage>(string queueName, TMessage message) where TMessage : MessageArgument
        {
            string newQueueName = GetNewQueueName(queueName, message.GroupName);
            //1.获取消息实际处理类
            var queue = pairs.GetOrAdd(newQueueName, (newQueueName) =>
            {
                return (MessageQueueHandler)Activator.CreateInstance(typeof(BlockingCollectionMQHandler<>).MakeGenericType(message.GetType()));
            });
            //2.获取实现消费者
            var consumers = _serviceFactory.GetServices<IConsumer<TMessage>>();
            var consumer = consumers.FirstOrDefault(c => c.QueueName.Equals(queueName));
            if (null == consumer)
                throw new InvalidOperationException($"无效的队列名称;{queueName}");
            //3.绑定消费之
            if ((!queue.IsBindReceivedEvent))
                queue.BindReceivedEvent(async message => await consumer.Handle((TMessage)message));
            return await queue.Publish(message);
        }

        private string GetNewQueueName(string queueName, string groupName)
        {
            string newQueueName = queueName;
            if (!string.IsNullOrEmpty(groupName))
                newQueueName = newQueueName + "_" + groupName;
            return newQueueName;
        }
    }
}
