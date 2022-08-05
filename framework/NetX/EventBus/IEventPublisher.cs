using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.EventBus;

/// <summary>
/// 事件发布接口
/// </summary>
public interface IEventPublisher
{
    Task PublishAsync(IEventSource eventSource, CancellationToken cancellationToken);
}
