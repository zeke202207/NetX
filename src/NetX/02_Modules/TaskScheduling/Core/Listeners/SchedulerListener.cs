using NetX.Ddd.Core;
using NetX.Common.Attributes;
using NetX.TaskScheduling.Domain;
using NetX.TaskScheduling.Model;
using Quartz;

namespace NetX.TaskScheduling.Core
{
    /// <summary>
    /// 调度任务监听器[停用]
    /// </summary>
    //[Scoped]
    public class SchedulerListener : ISchedulerListener
    {
        private readonly ICommandBus _jobtaskCommand;

        public SchedulerListener(ICommandBus jobtaskCommand)
        {
            this._jobtaskCommand = jobtaskCommand;
        }

        private async Task SendCommand(string name, string group, JobTaskState state)
        {
            await _jobtaskCommand.Send<SchedulerListenerCommand>(new SchedulerListenerCommand(name, group, state));
        }

        public async Task JobAdded(IJobDetail jobDetail, CancellationToken cancellationToken = default(CancellationToken))
        {
            // await SendCommand(jobDetail.Key.Name, jobDetail.Key.Group, JobTaskState.Started);
            await Task.CompletedTask;
        }

        public async Task JobDeleted(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            await SendCommand(jobKey.Name, jobKey.Group, JobTaskState.Deleted);
        }

        public async Task JobInterrupted(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            await SendCommand(jobKey.Name,jobKey.Group, JobTaskState.Interrupted);
        }

        public async Task JobPaused(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            await SendCommand(jobKey.Name, jobKey.Group, JobTaskState.Paused);
        }

        public async Task JobResumed(JobKey jobKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            await SendCommand(jobKey.Name, jobKey.Group, JobTaskState.Resumed);
        }

        #region do nothing

        public async Task JobsPaused(string jobGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task JobsResumed(string jobGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task JobScheduled(ITrigger trigger, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task JobUnscheduled(TriggerKey triggerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task SchedulerError(string msg, SchedulerException cause, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task SchedulerShutdown(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task SchedulerShuttingdown(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task SchedulerStarted(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task SchedulerInStandbyMode(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task SchedulerStarting(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task SchedulingDataCleared(CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task TriggerFinalized(ITrigger trigger, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task TriggerPaused(TriggerKey triggerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task TriggerResumed(TriggerKey triggerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task TriggersPaused(string triggerGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        public async Task TriggersResumed(string triggerGroup, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Task.CompletedTask;
        }

        #endregion
    }
}
