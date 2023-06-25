using NetX.Common.ModuleInfrastructure;
using NetX.TaskScheduling.Model;

namespace NetX.TaskScheduling.Core;

/// <summary>
/// 任务调度管理器
/// </summary>
public interface IScheduleService
{
    /// <summary>
    /// 添加一个新的计划任务
    /// </summary>
    /// <param name="scheduleModel">计划任务配置实体</param>
    /// <returns></returns>
    Task<ResultModel<bool>> AddJob(CronScheduleRequest scheduleModel);

    /// <summary>
    /// 暂停一个job任务
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="groupName"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> PauseJob(string jobId);

    /// <summary>
    /// 恢复一个已暂停的job任务
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="groupName"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> ResumeJob(string jobId);

    /// <summary>
    /// 删除一个job
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> DeleteJob(string jobId);

    /// <summary>
    /// 获取任务调度列表
    /// </summary>
    /// <param name="scheduleParam"></param>
    /// <returns></returns>
    Task<ResultModel<List<ScheduleModel>>> GetJob(ScheduleListParam scheduleParam);

    /// <summary>
    /// 根据任务id获取任务详情
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    Task<ResultModel<ScheduleModel>> GetJob(string jobId);

    /// <summary>
    /// 获取全部支持的jobtype
    /// </summary>
    /// <returns></returns>
    Task<ResultModel<List<SupportJobTypeModel>>> GetAllSupportJobType();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<ResultModel<bool>> EnabledJob(EnabledJobRequest request);
}
