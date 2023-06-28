using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetX.MemoryQueue
{
    internal class BlockingCollectionMQHandler<TMessage> : MessageQueueHandler
        where TMessage : MessageArgument
    {
        private BlockingCollection<TMessage> _queue;
        public Action<MessageArgument> _received;

        public BlockingCollectionMQHandler()
        {
            _queue = new BlockingCollection<TMessage>();
        }

        public override bool IsBindReceivedEvent
        {
            get => this._received != null;
        }

        public override void BindReceivedEvent(Action<MessageArgument> received)
        {
            this._received = received;
            if(null != _received)
            {
                Task.Factory.StartNew(() =>
                {
                    foreach(var message in _queue.GetConsumingEnumerable())
                    {
                        if (!_queue.IsAddingCompleted)
                        {
                            try
                            {
                                if (null != message && null != received)
                                    _received.Invoke(message);
                            }
                            catch (Exception _)
                            {
                                //Console.WriteLine(ex);
                            }
                        }
                        else
                        {
                            if (null != received)
                                received = null;
                        }
                    }
                });
            }
        }

        public override async Task<bool> Publish(MessageArgument message)
        {
            return await Task.Run<bool>(() =>
            {
                if (_queue.IsCompleted)
                    return true;
                _queue.Add((TMessage)message);
                return true;
            });
        }
    }
}
