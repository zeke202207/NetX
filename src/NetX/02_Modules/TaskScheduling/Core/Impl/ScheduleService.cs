using Netx.QuartzScheduling;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.TaskScheduling.Core.Impl;
using NetX.TaskScheduling.Model;

namespace NetX.TaskScheduling.Core;

/// <summary>
/// 任务调度管理类
/// </summary>
[Scoped]
public class ScheduleService : ScheduleQuartzService, IScheduleService
{
    /// <summary>
    /// 任务调度服务管理实例
    /// </summary>
    /// <param name="quartzServer">quartz服务</param>
    public ScheduleService(IQuartzServer quartzServer)
        : base(quartzServer)
    {
    }

    /// <summary>
    /// 添加任务
    /// </summary>
    /// <param name="scheduleModel"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> AddJob(ScheduleRequest scheduleModel)
    {
        //1. quartz add job
        await base.AddJob(scheduleModel);
        //2. database handle
        //TODO:数据库处理
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 暂停一个任务
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="groupName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> PauseJob(string jobName, string groupName)
    {
        await base.PauseJob(jobName, groupName);
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 恢复一个暂停任务
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="groupName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> ResumeJob(string jobName, string groupName)
    {
        await base.ResumeJob(jobName, groupName);
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 删除一个任务
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="groupName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> DeleteJob(string jobName, string groupName)
    {
        //1.Quartz删除job
        await base.DeleteJob(jobName, groupName);
        //2.数据库删除job
        //TODO:数据库处理
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 获取全部任务
    /// </summary>
    /// <param name="scheduleParam"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<ResultModel<PagerResultModel<List<ScheduleModel>>>> GetJob(ScheduleListParam scheduleParam)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 根据id获取任务
    /// </summary>
    /// <param name="jobId">数据库jobid</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<ResultModel<ScheduleModel>> GetJob(string jobId)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 根据任务名获取任务
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="groupName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public Task<ResultModel<ScheduleModel>> GetJob(string jobName, string groupName)
    {
        throw new NotImplementedException();
    }
}
