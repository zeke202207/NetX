using Microsoft.AspNetCore.Mvc;
using Netx.QuartzScheduling;
using NetX.Authentication.Core;
using NetX.Common.ModuleInfrastructure;
using NetX.Swagger;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiControllerDescription(TaskSchedulingConstEnum.C_TASKSCHEDULING_GROUPNAME, Description = "NetX实现的任务调度模块->任务调度管理")]
    public class JobTaskController : BaseController
    {
        private readonly IQuartzServer _quartzServer;

        /// <summary>
        /// 
        /// </summary>
        public JobTaskController(IQuartzServer quartzServer)
        {
            this._quartzServer = quartzServer;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <returns></returns>
        [ApiActionDescription("添加任务")]
        [NoPermission]
        [HttpPost]
        public async Task AddJob()
        {
            Dictionary<string, string> test = new()
            {
                {"a","1" },
                {"b","2" }
            };

           await this._quartzServer.AddJob(
                JobBuilder.Create(Type.GetType("NetX.Tools.Jobs.RestoreDatabaseJob"))
                .WithIdentity("a", "b")
                .SetJobData(new JobDataMap(test))
                .Build()
                ,
                TriggerBuilder.Create()
                .WithIdentity("a", "b")
                .WithDescription("test")
                .WithCronSchedule("0/5 * * * * ?")
                .Build()
                );

        }
    }
}
