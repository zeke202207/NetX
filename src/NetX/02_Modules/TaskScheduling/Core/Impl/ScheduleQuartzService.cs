using Netx.QuartzScheduling;
using NetX.Common.ModuleInfrastructure;
using NetX.TaskScheduling.Model;
using Quartz;

namespace NetX.TaskScheduling.Core.Impl
{
    /// <summary>
    /// Quartz 调度管理
    /// </summary>
    public abstract class ScheduleQuartzService : BaseService
    {
        private readonly IQuartzServer _quartzServer;

        /// <summary>
        /// Quartz调度管理器
        /// </summary>
        public ScheduleQuartzService(IQuartzServer quartzServer)
        {
            this._quartzServer = quartzServer;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="scheduleModel"></param>
        public virtual async Task AddJob(ScheduleRequest scheduleModel)
        {
            await this._quartzServer.AddJob(
                 JobBuilder.Create(Type.GetType(scheduleModel.Job.JobType))
                 .WithIdentity(scheduleModel.Job.Name, scheduleModel.Job.Group)
                 .SetJobData(new JobDataMap(scheduleModel.Job.JobDataMap))
                 .Build()
                 ,
                 CreateTriggerBuilder(scheduleModel)
                 .Build()
                 );
        }

        /// <summary>
        /// 暂停job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public virtual async Task PauseJob(string jobName, string groupName)
        {
            await this._quartzServer.PauseJob(new JobKey(jobName, groupName));
        }

        /// <summary>
        /// 恢复job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public virtual async Task ResumeJob(string jobName, string groupName)
        {
            await this._quartzServer.ResumeJob(new JobKey(jobName, groupName));
        }

        /// <summary>
        /// 删除job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public virtual async Task DeleteJob(string jobName, string groupName)
        {
            await this._quartzServer.DeleteJob(new JobKey(jobName, groupName));
        }

        /// <summary>
        /// 构建触发器
        /// </summary>
        /// <param name="scheduleModel"></param>
        /// <returns></returns>
        protected virtual TriggerBuilder CreateTriggerBuilder(ScheduleRequest scheduleModel) => scheduleModel.Trigger switch
        {
            CronTriggerRequest trigger => TriggerBuilder.Create()
                                 .WithIdentity(scheduleModel.Trigger.Name)
                                 .WithDescription(scheduleModel.Trigger.Description)
                                 .WithCronSchedule(trigger.CronExpression),
            _ => TriggerBuilder.Create(),
        };
    }
}
