using NetX.EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1
{
    public class EventHandler : IEventSubscriber
    {
        [EventSubscribe("zeke")]
        public async Task Handler(EventHandlerExecutingContext context)
        {
            Console.WriteLine("ok");
            await Task.CompletedTask;
        }
    }

    public class EventHandler1 : IEventSubscriber
    {
        [EventSubscribe("zeke")]
        public async Task Handler(EventHandlerExecutingContext context)
        {
            Console.WriteLine("ok---2");
            await Task.CompletedTask;
        }
    }
}
