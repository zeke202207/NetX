using Netx.QuartzScheduling;
using NetX.Common.Attributes;
using NetX.TaskScheduling.Model;
using Quartz;
using Quartz.Util;

namespace NetX.TaskScheduling.Core.Impl
{
    /// <summary>
    /// Quartz 调度管理
    /// </summary>
    [Scoped]
    public class QuartzSchedule : ISchedule
    {
        private readonly IQuartzServer _quartzServer;

        /// <summary>
        /// Quartz调度管理器
        /// </summary>
        public QuartzSchedule(IQuartzServer quartzServer)
        {
            this._quartzServer = quartzServer;
        }

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="scheduleModel"></param>
        public async Task AddJobAsync(JobTaskModel scheduleModel)
        {
            if (!scheduleModel.Enabled)
                return;
            var type = JobTaskTypeManager.Instance.Get(scheduleModel.JobType);
            var jobBuilder = JobBuilder.Create(type)
                     .WithIdentity(scheduleModel.Name, scheduleModel.Group)
                     .WithDescription(scheduleModel.Description);
            if (null != scheduleModel.JobDataMap)
                jobBuilder.SetJobData(new JobDataMap(scheduleModel.JobDataMap));
            var jobDetail = jobBuilder
                     .Build();
            await this._quartzServer.AddJob(
               jobDetail
                 ,
                 CronTriggerBuilder(scheduleModel.Trigger)
                    .Build()
                 );
        }

        /// <summary>
        /// 暂停job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task PauseJobAsync(string jobName, string groupName)
        {
            await this._quartzServer.PauseJob(new JobKey(jobName, groupName));
        }

        /// <summary>
        /// 恢复job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task ResumeJobAsync(string jobName, string groupName)
        {
            await this._quartzServer.ResumeJob(new JobKey(jobName, groupName));
        }

        /// <summary>
        /// 删除job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task DeleteJobAsync(string jobName, string groupName)
        {
            await this._quartzServer.DeleteJob(new JobKey(jobName, groupName));
        }


        /// <summary>
        /// 构建cron触发器
        /// </summary>
        /// <param name="cron"></param>
        /// <returns></returns>
        private TriggerBuilder CronTriggerBuilder(CronJobTaskTriggerModel cron)
        {
            var trigger = TriggerBuilder.Create()
                                        .WithPriority(cron.Priority)
                                        .WithIdentity(cron.Name)
                                        .WithCronSchedule(cron.CronExpression)
                                        .WithDescription(cron.Description);
            if (cron.StartAt.HasValue)
                trigger = trigger.StartAt(cron.StartAt.Value);
            if (cron.EndAt.HasValue)
                trigger = trigger.EndAt(cron.EndAt.Value);
            if (cron.StartNow)
                trigger = trigger.StartNow();
            return trigger;
        }
    }
}
