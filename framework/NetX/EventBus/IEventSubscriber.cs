using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.EventBus;

/// <summary>
/// 事件的订阅者
/// </summary>
public interface IEventSubscriber
{
    /// <summary>
    /// 时间处理程序
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    [EventSubscribe("00000000-0000-0000-0000-000000000000")]
    public Task Handler(EventHandlerExecutingContext context);
}
