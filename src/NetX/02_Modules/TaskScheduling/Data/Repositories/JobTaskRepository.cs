using FreeSql;
using Google.Protobuf.WellKnownTypes;
using NetX.Common.Attributes;
using NetX.TaskScheduling.DatabaseSetup.CreateTable;
using NetX.TaskScheduling.Model;
using NetX.TaskScheduling.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Quartz.Logging.OperationName;

namespace NetX.TaskScheduling.Data.Repositories
{
    /// <summary>
    /// api 仓储服务
    /// </summary>
    [Scoped]
    public class JobTaskRepository : BaseRepository<sys_jobtask, string>
    {
        private readonly IFreeSql _freeSql;

        /// <summary>
        /// jobtask 仓储对象实例
        /// </summary>
        /// <param name="fsql">ORM实例</param>
        public JobTaskRepository(IFreeSql fsql)
            : base(fsql, null, null)
        {
            this._freeSql = fsql;
        }

        /// <summary>
        /// 添加一个任务计划 
        /// </summary>
        /// <param name="job"></param>
        /// <param name="trigger"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<bool> AddJob(sys_jobtask job, sys_trigger trigger)
        {
            bool result = true;
            using (var uow = this._freeSql.CreateUnitOfWork())
            {
                try
                {
                    var jobtasktriggerRep = uow.GetRepository<sys_jobtask_trigger>();
                    var jobtaskRep = uow.GetRepository<sys_jobtask>();
                    var triggerRep = uow.GetRepository<sys_trigger>();
                    await jobtaskRep.InsertAsync(job);
                    await triggerRep.InsertAsync(trigger);
                    await jobtasktriggerRep.InsertAsync(new sys_jobtask_trigger() { jobtaskid = job.id, triggerid = trigger.id });
                    uow.Commit();
                }
                catch (Exception ex)
                {
                    result = false;
                    uow.Rollback();
                    throw new Exception("添加job失败", ex);
                }
            }
            return result;
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="jobName"></param>
        /// <param name="jobGroup"></param>
        /// <returns></returns>
        public async Task<bool> DeleteJob(string jobName, string jobGroup)
        {
            bool result = true;
            using (var uow = this._freeSql.CreateUnitOfWork())
            {
                try
                {
                    var jobtasktriggerRep = uow.GetRepository<sys_jobtask_trigger>();
                    var jobtaskRep = uow.GetRepository<sys_jobtask>();
                    var triggerRep = uow.GetRepository<sys_trigger>();
                    var jobEntity = await jobtaskRep.Select.Where(p => p.name.ToLower() == jobName.ToLower() && p.group.ToLower() == jobGroup.ToLower()).FirstAsync();
                    var jobtasktriggerEntity = await jobtasktriggerRep.Select.Where(p => p.jobtaskid == jobEntity.id).FirstAsync();
                    var triggerEntity = await triggerRep.Select.Where(p=>p.id == jobtasktriggerEntity.triggerid).FirstAsync();
                    await jobtasktriggerRep.DeleteAsync(jobtasktriggerEntity);
                    await jobtaskRep.DeleteAsync(jobEntity);
                    await triggerRep.DeleteAsync(triggerEntity);
                    uow.Commit();
                }
                catch (Exception ex)
                {
                    result = false;
                    uow.Rollback();
                    throw new Exception("job删除失败", ex);
                }
            }
            return result;
        }
    }
}
