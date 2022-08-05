using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.EventBus;

/// <summary>
/// 事件处理后上下文
/// </summary>
public sealed class EventHandlerExecutedContext : EventHandlerContext
{
    /// <summary>
    /// 事件处理后上下文实例
    /// </summary>
    /// <param name="source">事件源</param>
    /// <param name="properties">共享上下文数据</param>
    public EventHandlerExecutedContext(IEventSource source, IDictionary<object, object> properties) 
        : base(source, properties)
    {
    }

    /// <summary>
    /// 执行后时间
    /// </summary>
    public DateTime ExcutedTime { get; internal set; }

    /// <summary>
    /// 异常信息
    /// </summary>
    public InvalidOperationException Exception { get; internal set; }
}
