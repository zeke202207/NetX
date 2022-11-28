using Microsoft.AspNetCore.Mvc;
using Netx.QuartzScheduling;
using NetX.Authentication.Core;
using NetX.Common.ModuleInfrastructure;
using NetX.Swagger;
using NetX.TaskScheduling.Core;
using NetX.TaskScheduling.Model;
using Quartz;

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
    public async Task<ResultModel> AddCronJob(ScheduleRequest requestDto)
    {
#if DEBUG
        Dictionary<string, string> test = new()
        {
            {"a","1" },
            {"b","2" }
        };
        requestDto = new Model.ScheduleRequest()
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
}
