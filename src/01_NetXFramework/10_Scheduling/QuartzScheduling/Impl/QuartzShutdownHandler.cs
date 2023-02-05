using Microsoft.Extensions.Logging;
using NetX.Common;
using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Netx.QuartzScheduling;

/// <summary>
/// 调度服务器关闭处理器
/// </summary>
[Singleton]
public class QuartzShutdownHandler : IShutdownHandler
{
    private readonly IQuartzServer _server;
    private readonly IJobTaskLogger _logger;

    /// <summary>
    /// 调度服务关闭处理器
    /// </summary>
    /// <param name="server"></param>
    /// <param name="logger"></param>
    public QuartzShutdownHandler(IQuartzServer server,IJobTaskLogger logger)
    {
        _server = server;
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task Handle()
    {
        if (_server != null)
            await _server.Stop();
        await _logger.Debug("Quartz stopped");
    }
}
