using NetX.TaskScheduling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Task AddJob(ScheduleRequest scheduleModel);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        Task PauseJob(string jobName, string groupName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        Task ResumeJob(string jobName, string groupName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        Task DeleteJob(string jobName, string groupName);
    }
}
