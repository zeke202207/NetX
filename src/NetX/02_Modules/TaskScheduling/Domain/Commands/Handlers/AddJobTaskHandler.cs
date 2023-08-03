using Microsoft.EntityFrameworkCore;
using NetX.Ddd.Domain;
using NetX.Common.Attributes;
using NetX.TaskScheduling.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Domain
{
    [Scoped]
    public class AddJobTaskHandler : DomainCommandHandler<AddJobTaskCommand>
    {
        private readonly IUnitOfWork _uow;

        public AddJobTaskHandler(
            IUnitOfWork uow)
        {
            _uow = uow;
        }

        public override async Task<bool> Handle(AddJobTaskCommand request, CancellationToken cancellationToken)
        {
            var jobTask = new sys_jobtask()
            {
                Id = request.Id,
                createtime = request.CreateTime,
                name = request.Name,
                group = request.Group,
                jobtype = request.JobType,
                description = request.Description,
                disallowconcurrentexecution = request.DisAllowConcurrentExecution,
                datamap = request.DataMap,
                state = request.State,
                enabled = Convert.ToInt32(request.Enabled)
            };
            var trigger = GetTriggerEntity(request.Request);
            trigger.jobtaskid = jobTask.Id;
            bool result = true;
            using var transaction = await _uow.BeginTransactionAsync();
            try
            {
                await _uow.GetRepository<sys_jobtask, string>().AddAsync(jobTask);
                await _uow.SaveChangesAsync(false);
                await _uow.GetRepository<sys_jobtasktrigger, string>().AddAsync(trigger);
                await _uow.SaveChangesAsync(true);
                await _uow.CommitTransactionAsync(transaction);
            }
            catch (Exception ex)
            {
                result = false;
                throw new Exception("事务保存任务失败", ex);
            }
            finally
            {
                if(!result)
                    await _uow.RollbackAsync(transaction);                    
            }
            return result;
        }


        /// <summary>
        /// 构建触发器
        /// </summary>
        /// <param name="scheduleModel"></param>
        /// <returns></returns>
        private sys_jobtasktrigger GetTriggerEntity(CronScheduleRequest cron)
        {
            return new sys_jobtasktrigger()
            {
                Id = Guid.NewGuid().ToString("N"),
                createtime = DateTime.UtcNow,
                description = cron.Trigger.Description,
                endat = cron.Trigger.EndAt,
                startat = cron.Trigger.StartAt,
                name = cron.Trigger.Name,
                priority = cron.Trigger.Priority,
                startnow = cron.Trigger.StartNow,
                cron = cron.Trigger.CronExpression,
                triggertype = 0
            };
        }
    }
}
