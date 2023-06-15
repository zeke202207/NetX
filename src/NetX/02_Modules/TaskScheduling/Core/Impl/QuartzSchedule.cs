using Netx.QuartzScheduling;
using NetX.Common.Attributes;
using NetX.TaskScheduling.Model;
using NetX.TaskScheduling.Model.Dtos.RequestDto;
using Quartz;

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
        public async Task AddJob(ScheduleRequest scheduleModel)
        {
            await this._quartzServer.AddJob(
                 JobBuilder.Create(Type.GetType(scheduleModel.Job.JobType))
                     .WithIdentity(scheduleModel.Job.Name, scheduleModel.Job.Group)
                     .SetJobData(new JobDataMap(scheduleModel.Job.JobDataMap))
                     .WithDescription(scheduleModel.Job.Description)
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
        public async Task PauseJob(string jobName, string groupName)
        {
            await this._quartzServer.PauseJob(new JobKey(jobName, groupName));
        }

        /// <summary>
        /// 恢复job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task ResumeJob(string jobName, string groupName)
        {
            await this._quartzServer.ResumeJob(new JobKey(jobName, groupName));
        }

        /// <summary>
        /// 删除job
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public async Task DeleteJob(string jobName, string groupName)
        {
            await this._quartzServer.DeleteJob(new JobKey(jobName, groupName));
        }

        /// <summary>
        /// 构建触发器
        /// </summary>
        /// <param name="scheduleModel"></param>
        /// <returns></returns>
        private TriggerBuilder CreateTriggerBuilder(ScheduleRequest scheduleModel) => scheduleModel switch
        {
            CronScheduleRequest cron => CronTriggerBuilder(cron),
            SimpleScheduleRequest simple => SimpleTriggerBuilder(simple),
            _ => TriggerBuilder.Create()
        };

        /// <summary>
        /// 构建cron触发器
        /// </summary>
        /// <param name="cron"></param>
        /// <returns></returns>
        private TriggerBuilder CronTriggerBuilder(CronScheduleRequest cron)
        {
            var trigger = TriggerBuilder.Create()
                                        .WithPriority(cron.Trigger.Priority)
                                        .WithIdentity(cron.Trigger.Name)
                                        .WithCronSchedule(cron.Trigger.CronExpression)
                                        .WithDescription(cron.Trigger.Description);
            if (cron.Trigger.StartAt.HasValue)
                trigger = trigger.StartAt(cron.Trigger.StartAt.Value);
            if (cron.Trigger.EndAt.HasValue)
                trigger = trigger.EndAt(cron.Trigger.EndAt.Value);
            if (cron.Trigger.StartNow)
                trigger = trigger.StartNow();
            return trigger;
        }

        /// <summary>
        /// 构建simple触发器
        /// </summary>
        /// <param name="simple"></param>
        /// <returns></returns>
        private TriggerBuilder SimpleTriggerBuilder(SimpleScheduleRequest simple)
        {
            return TriggerBuilder.Create()
                                .WithDescription("待实现");
        }
    }
}
