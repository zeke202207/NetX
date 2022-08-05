using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.EventBus;

/// <summary>
/// 事件处理执行前上下文
/// </summary>
public sealed class EventHandlerExecutingContext : EventHandlerContext
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="properties"></param>
    public EventHandlerExecutingContext(IEventSource source, IDictionary<object, object> properties) 
        : base(source, properties)
    {
    }

    /// <summary>
    /// 执行前时间
    /// </summary>
    public DateTime ExecutingTime { get; internal set; }
}
