using Netx.QuartzScheduling;

namespace NetX.DemoJob;

/// <summary>
/// 数据库定时还原job，用于演示数据库
/// </summary>
public class ZekeTestJob : JobTask
{
    /// <summary>
    /// 数据库还原job实例
    /// </summary>
    /// <param name="logger">日志对象</param>
    public ZekeTestJob(IJobTaskLogger logger)
        : base(logger)
    {
    }

    /// <summary>
    /// 任务入口
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public override async Task Execute(JobTaskExecutionContext context)
    {
        await Task.CompletedTask;
    }
}
