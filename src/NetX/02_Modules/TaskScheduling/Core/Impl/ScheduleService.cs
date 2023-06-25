using MediatR;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Netx.Ddd.Core;
using Netx.QuartzScheduling;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.TaskScheduling.DatabaseSetup;
using NetX.TaskScheduling.Domain;
using NetX.TaskScheduling.Domain.Commands;
using NetX.TaskScheduling.Model;
using Newtonsoft.Json;

namespace NetX.TaskScheduling.Core;

/// <summary>
/// 任务调度管理类
/// </summary>
[Scoped]
public class ScheduleService : BaseService, IScheduleService
{
    private readonly ICommandBus _jobtaskCommand;
    private readonly IQueryBus _jobtaskQuery;
    private readonly ISchedule _schedule;

    /// <summary>
    /// 任务调度服务管理实例
    /// </summary>
    public ScheduleService(ICommandBus jobtaskCommand, IQueryBus queryBus,  ISchedule schedule)
        : base()
    {
        this._schedule = schedule;
        this._jobtaskCommand = jobtaskCommand;
        _jobtaskQuery = queryBus;
    }

    /// <summary>
    /// 添加任务
    /// </summary>
    /// <param name="scheduleModel"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> AddJob(CronScheduleRequest scheduleModel)
    {
        //先执行数据库操作，以便于更新数据库任务执行状态
        //2. database handle
        await _jobtaskCommand.Send<AddJobTaskCommand>(new AddJobTaskCommand(
             Guid.NewGuid().ToString("N"),
             scheduleModel.Job.Name,
             scheduleModel.Job.Group,
             scheduleModel.Job.JobType,
             scheduleModel.Job.JobDataMap,
             scheduleModel.Job.DisAllowConcurrentExecution,
             DateTime.UtcNow,
             scheduleModel.Job.Description,
             scheduleModel.Job.Enabled,
             scheduleModel.Job.State,
             scheduleModel
            ));
        //1. quartz add job
        var dataMap = new Dictionary<string, string>();
        if (!string.IsNullOrWhiteSpace(scheduleModel.Job.JobDataMap))
            dataMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(scheduleModel.Job.JobDataMap);
        await this._schedule.AddJobAsync(new JobTaskModel()
        {
             Name = scheduleModel.Job.Name,
             Group = scheduleModel.Job.Group,
             JobType = scheduleModel.Job.JobType,
             JobDataMap = dataMap,
             DisAllowConcurrentExecution = scheduleModel.Job.DisAllowConcurrentExecution,
             Description = scheduleModel.Job.Description,
             Enabled = scheduleModel.Job.Enabled,
             State = JobTaskState.None,
             Trigger = CreateTriggerBuilder(scheduleModel)
        });
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 暂停一个任务
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="groupName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> PauseJob(string jobId)
    {
        var jobtask = await _jobtaskQuery.Send<JobTaskQueryById, sys_jobtask>(new JobTaskQueryById(jobId));
        if (null == jobtask)
            return base.Success<bool>(true);
        await this._schedule.PauseJobAsync(jobtask.name, jobtask.group);
        await this._jobtaskCommand.Send<StateJobCommand>(new StateJobCommand(jobId, 2));
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 恢复一个暂停任务
    /// </summary>
    /// <param name="jobName"></param>
    /// <param name="groupName"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> ResumeJob(string jobId)
    {
        var jobtask = await _jobtaskQuery.Send<JobTaskQueryById, sys_jobtask>(new JobTaskQueryById(jobId));
        if (null == jobtask)
            return base.Success<bool>(true);
        await this._schedule.ResumeJobAsync(jobtask.name, jobtask.group);
        await this._jobtaskCommand.Send<StateJobCommand>(new StateJobCommand(jobId, 1));
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
        var jobtask = await _jobtaskQuery.Send<JobTaskQueryById, sys_jobtask>(new JobTaskQueryById(jobId));
        if(null == jobtask)
            return base.Success<bool>(true);
        await this._schedule.DeleteJobAsync(jobtask.name, jobtask.group);
        await _jobtaskCommand.Send<RemoveJobTaskCommand>(new RemoveJobTaskCommand(jobId));
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 获取全部任务
    /// </summary>
    /// <param name="scheduleParam"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<List<ScheduleModel>>> GetJob(ScheduleListParam scheduleParam)
    {
        var jobtasks = await _jobtaskQuery.Send<JobTaskQueryAll, IEnumerable<JobTaskModel>>(new JobTaskQueryAll(scheduleParam.JobName));
        if (null == jobtasks)
            return base.Success<List<ScheduleModel>>(new List<ScheduleModel>());
        var result = jobtasks.Select(p => ToScheduleModel(p));
        return base.Success<List<ScheduleModel>>(result.ToList());
    }

    /// <summary>
    /// 根据id获取任务
    /// </summary>
    /// <param name="jobId">数据库jobid</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<ScheduleModel>> GetJob(string jobId)
    {
        var jobtasks = await _jobtaskQuery.Send<JobTaskQueryAll, IEnumerable<JobTaskModel>>(new JobTaskQueryAll(string.Empty));
        var jobtask = jobtasks.FirstOrDefault(p => p.Id == jobId);
        if (null == jobtask)
            return base.Error<ScheduleModel>($"未找到{jobId}的任务");
        ScheduleModel model = ToScheduleModel(jobtask);
        return base.Success<ScheduleModel>(model);
    }

    /// <summary>
    /// 获取全部支持的任务
    /// </summary>
    /// <returns></returns>
    public async Task<ResultModel<List<SupportJobTypeModel>>> GetAllSupportJobType()
    {
        var result = JobTaskTypeManager.Instance.GetAll().ToList();
        List<SupportJobTypeModel> list = new List<SupportJobTypeModel>();
        result.ForEach(p => list.Add(new SupportJobTypeModel() { TypeName = p }));
        return base.Success<List<SupportJobTypeModel>>(list);
    }

    /// <summary>
    /// 启用/禁用job
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<bool>> EnabledJob(EnabledJobRequest request)
    {
        if (!request.Enabled)
        {
            var jobtask = await _jobtaskQuery.Send<JobTaskQueryById, sys_jobtask>(new JobTaskQueryById(request.Id));
            if (null != jobtask)
                await this._schedule.PauseJobAsync(jobtask.name, jobtask.group);
        }
        await this._jobtaskCommand.Send<EnabledJobCommand>(new EnabledJobCommand(request.Id, request.Enabled));
        return base.Success<bool>(true);
    }

    /// <summary>
    /// 创建触发器
    /// </summary>
    /// <param name="scheduleModel"></param>
    /// <returns></returns>
    private CronJobTaskTriggerModel? CreateTriggerBuilder(CronScheduleRequest cron)
    {
        return new CronJobTaskTriggerModel()
        {
            Name = cron.Trigger.Name,
            CronExpression = cron.Trigger.CronExpression,
            Description = cron.Trigger.Description,
            EndAt = cron.Trigger.EndAt,
            Priority = cron.Trigger.Priority,
            StartAt = cron.Trigger.StartAt,
            StartNow = cron.Trigger.StartNow
        };
    }

    /// <summary>
    /// 实体对象转换
    /// </summary>
    /// <param name="jobtask"></param>
    /// <returns></returns>
    private ScheduleModel ToScheduleModel(JobTaskModel jobtask)
    {
        return new ScheduleModel()
        {
            Id = jobtask.Id,
            Description = jobtask.Description,
            DisAllowConcurrentExecution = jobtask.DisAllowConcurrentExecution,
            Group = jobtask.Group,
            JobDataMap = jobtask.JobDataMap,
            JobType = jobtask.JobType,
            Name = jobtask.Name,
            Enabled = jobtask.Enabled,
            State = jobtask.State,
            Trigger = new ScheduleTriggerModel()
            {
                Name = jobtask.Trigger.Name,
                CronExpression = jobtask.Trigger.CronExpression,
                Description = jobtask.Trigger.Description,
                EndAt = jobtask.Trigger.EndAt,
                Priority = jobtask.Trigger.Priority,
                StartAt = jobtask.Trigger.StartAt,
                StartNow = jobtask.Trigger.StartNow
            }
        };
    }

}
