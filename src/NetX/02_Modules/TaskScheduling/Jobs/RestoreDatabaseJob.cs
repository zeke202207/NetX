using Netx.QuartzScheduling;

namespace NetX.Tools.Jobs;

/// <summary>
/// 数据库定时还原job，用于演示数据库
/// </summary>
public class RestoreDatabaseJob : JobTask
{
    /// <summary>
    /// 数据库还原job实例
    /// </summary>
    /// <param name="logger">日志对象</param>
    public RestoreDatabaseJob(IJobTaskLogger logger)
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
