using NetX.TaskScheduling.Model;

namespace NetX.TaskScheduling.Core
{
    /// <summary>
    /// 计划任务接口
    /// </summary>
    public interface ISchedule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scheduleModel"></param>
        /// <returns></returns>
        Task AddJobAsync(JobTaskModel scheduleModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        Task PauseJobAsync(string jobName, string groupName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        Task ResumeJobAsync(string jobName, string groupName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        Task DeleteJobAsync(string jobName, string groupName);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        Task TriggerJobAsync(string jobName, string groupName);
    }
}
