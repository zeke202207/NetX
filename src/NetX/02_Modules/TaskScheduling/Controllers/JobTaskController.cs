using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common.ModuleInfrastructure;
using NetX.Swagger;
using NetX.TaskScheduling.Core;
using NetX.TaskScheduling.Model;

namespace NetX.TaskScheduling.Controllers;

/// <summary>
/// 
/// </summary>
[ApiControllerDescription(TaskSchedulingConstEnum.C_TASKSCHEDULING_GROUPNAME, Description = "NetX实现的任务调度模块->任务调度管理")]
public class JobTaskController : BaseController
{
    private readonly IScheduleService _scheduleServer;

    /// <summary>
    /// 
    /// </summary>
    public JobTaskController(IScheduleService scheduleServer)
    {
        this._scheduleServer = scheduleServer;
    }

    /// <summary>
    /// 添加任务
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("添加任务")]
    [NoPermission]
    [HttpPost]
    public async Task<ResultModel> AddCronJob(CronScheduleRequest requestDto)
    {
#if DEBUG
        Dictionary<string, string> test = new()
        {
            {"a","1" },
            {"b","2" }
        };
        requestDto = new Model.CronScheduleRequest()
        {
            Id = Guid.NewGuid().ToString(),
            Job = new Model.JobRequest()
            {
                Name = "zeke",
                Group = "zekegroup",
                Description = "xxx",
                JobDataMap = test,
                JobType = "NetX.Tools.Jobs.RestoreDatabaseJob"
            },
            Trigger = new Model.CronTriggerRequest()
            {
                Name = "zeke trigger",
                Description = "yyy",
                CronExpression = "0/5 * * * * ?"
            }
        };
#endif
        return await this._scheduleServer.AddJob(requestDto);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [ApiActionDescription("暂停任务")]
    [NoPermission]
    [HttpPost]
    public async Task<ResultModel> PauseJob(JobIdentRequest requestDto)
    {
        return await this._scheduleServer.PauseJob(requestDto.JobName, requestDto.GroupName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [ApiActionDescription("恢复任务")]
    [NoPermission]
    [HttpPost]
    public async Task<ResultModel> ResumeJob(JobIdentRequest requestDto)
    {
        return await this._scheduleServer.ResumeJob(requestDto.JobName, requestDto.GroupName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [ApiActionDescription("删除任务")]
    [NoPermission]
    [HttpPost]
    public async Task<ResultModel> RemoveJob(JobIdentRequest requestDto)
    {
        return await this._scheduleServer.DeleteJob(requestDto.JobName, requestDto.GroupName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [ApiActionDescription("获取任务列表")]
    [NoPermission]
    [HttpPost]
    public async Task<ResultModel> GetJobList(ScheduleListParam requestDto)
    {
        return await this._scheduleServer.GetJob(requestDto);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="requestDto"></param>
    /// <returns></returns>
    [ApiActionDescription("通过任务名和分组名获取任务")]
    [NoPermission]
    [HttpPost]
    public async Task<ResultModel> GetJobByNameGroup(JobIdentRequest requestDto)
    {
        return await this._scheduleServer.GetJob(requestDto.JobName, requestDto.GroupName);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [ApiActionDescription("通过id获取任务")]
    [NoPermission]
    [HttpPost]
    public async Task<ResultModel> GetJobById(KeyParam key)
    {
        return await this._scheduleServer.GetJob(key.Id);
    }
}
