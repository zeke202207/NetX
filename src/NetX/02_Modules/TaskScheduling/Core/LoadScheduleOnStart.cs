using Microsoft.Extensions.Hosting;

namespace NetX.TaskScheduling.Core;

/// <summary>
/// 程序启动加载数据库任务
/// </summary>
internal class LoadScheduleOnStart : IHostedService
{
    public LoadScheduleOnStart()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task StartAsync(CancellationToken cancellationToken)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task StopAsync(CancellationToken cancellationToken)
    {

    }
}
