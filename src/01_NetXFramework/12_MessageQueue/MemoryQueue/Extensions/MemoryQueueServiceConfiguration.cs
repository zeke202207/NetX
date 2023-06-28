using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MemoryQueue
{
    public class MemoryQueueServiceConfiguration
    {
        public Type QueueImplementationType { get;private set; }
        public ServiceLifetime Lifetime { get; private set; }

        public MemoryQueueServiceConfiguration()
        {
            Lifetime = ServiceLifetime.Transient;
            QueueImplementationType = typeof(MemoryMessageQueue);
        }

        public MemoryQueueServiceConfiguration Using<TQueue>() where TQueue :IPublisher
        {
            QueueImplementationType = typeof (TQueue);
            return this;
        }

        public MemoryQueueServiceConfiguration AsSingleton()
        {
            Lifetime = ServiceLifetime.Singleton;
            return this;
        }

        public MemoryQueueServiceConfiguration AsTransient()
        {
            Lifetime = ServiceLifetime.Transient;
            return this;
        }

        public MemoryQueueServiceConfiguration AsScoped()
        {
            Lifetime = ServiceLifetime.Scoped;
            return this;
        }
    }
}
