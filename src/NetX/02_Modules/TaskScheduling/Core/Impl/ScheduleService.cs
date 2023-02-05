using FreeSql;
using Netx.QuartzScheduling;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.TaskScheduling.Core.Impl;
using NetX.TaskScheduling.Data.Repositories;
using NetX.TaskScheduling.Model;
using NetX.TaskScheduling.Model.Dtos.RequestDto;
using Newtonsoft.Json;
using Quartz;

namespace NetX.TaskScheduling.Core;

/// <summary>
/// 任务调度管理类
/// </summary>
[Scoped]
public class ScheduleService : BaseService, IScheduleService
{
    private readonly IBaseRepository<sys_jobtask> _jobTaskRepository;
    private readonly ISchedule _schedule;

    /// <summary>
    /// 任务调度服务管理实例
    /// </summary>
    public ScheduleService(IBaseRepository<sys_jobtask> jobTaskRepository, ISchedule schedule)
        : base()
    {
        this._jobTaskRepository = jobTaskRepository;
        this._schedule = schedule;
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
        await this._schedule.AddJob(scheduleModel);
        //2. database handle
        var result = await ((JobTaskRepository)this._jobTaskRepository).AddJob(
            new sys_jobtask()
            {
                id = base.CreateId(),
                createtime = base.CreateInsertTime(),
                name = scheduleModel.Job.Name,
                group = scheduleModel.Job.Group,
                jobtype = scheduleModel.Job.JobType,
                description = scheduleModel.Job.Description,
                disallowconcurrentexecution = scheduleModel.Job.DisAllowConcurrentExecution,
                datamap = JsonConvert.SerializeObject(scheduleModel.Job.JobDataMap),
            },
           GetTriggerEntity(scheduleModel));
        //TODO:数据库处理
        return base.Success<bool>(result);
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
        await this._schedule.PauseJob(jobName, groupName);
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
        await this._schedule.ResumeJob(jobName, groupName);
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 删除一个任务
    /// </summary>
    /// <param name="jobId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> DeleteJob(string jobId)
    {
        var job = await _jobTaskRepository.Select.Where(p=>p.id == jobId).FirstAsync();
        if(null == job)
            return base.Success<bool>(false);
        return await DeleteJob(job.name, job.group);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="groupName"></param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> DeleteJob(string jobName, string groupName)
    {
        //1.Quartz删除job
        await this._schedule.DeleteJob(jobName, groupName);
        //2.数据库删除job
        var result = await ((JobTaskRepository)this._jobTaskRepository).DeleteJob(jobName, groupName);
        return base.Success<bool>(result);
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
    public async Task<ResultModel<ScheduleModel>> GetJob(string jobName, string groupName)
    {
        //var entity = await this._jobTaskRepository.Where(p=>p.name.ToLower() == jobName.ToLower() && p.group.ToLower() == groupName.ToLower()).FirstAsync();
        //if (null == entity)
        //    return base.Error<ScheduleModel>("没有找到该任务");
        //return base.Success<ScheduleModel>(new ScheduleModel()
        //{

        //});
        throw new NotImplementedException();
    }

    /// <summary>
    /// 构建触发器
    /// </summary>
    /// <param name="scheduleModel"></param>
    /// <returns></returns>
    private sys_trigger GetTriggerEntity(ScheduleRequest scheduleModel) => scheduleModel switch
    {
        CronScheduleRequest cron => CronTrigger(cron),
        SimpleScheduleRequest simple => SimpleTrigger(simple),
        _ => throw new ArgumentNullException(nameof(scheduleModel)),
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cron"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private sys_trigger CronTrigger(CronScheduleRequest cron)
    {
        return new sys_trigger()
        {
            id = base.CreateId(),
            createtime = base.CreateInsertTime(),
            description = cron.Trigger.Description,
            endat = cron.Trigger.EndAt,
            startat = cron.Trigger.StartAt,
            name = cron.Trigger.Name,
            priority = cron.Trigger.Priority,
            startnow = cron.Trigger.StartNow,
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="simple"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private sys_trigger SimpleTrigger(SimpleScheduleRequest simple)
    {
        throw new NotImplementedException();
    }
}
