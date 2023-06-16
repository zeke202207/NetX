using Microsoft.EntityFrameworkCore;
using Netx.Ddd.Domain;
using NetX.Common.Attributes;
using NetX.TaskScheduling.Model;
using NetX.TaskScheduling.Model.Dtos.RequestDto;
using NetX.TaskScheduling.Model.Entity;
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
                datamap = JsonConvert.SerializeObject(request.DataMap),
            };
            var trigger = GetTriggerEntity(request.Request);
            var jobtasktrigger = new sys_jobtask_trigger()
            {
                jobtaskid = jobTask.Id,
                triggerid = trigger.Id,
            };
            bool result = true;
            using var transaction = await _uow.BeginTransactionAsync();
            try
            {
                await _uow.GetRepository<sys_jobtask, string>().AddAsync(jobTask);
                await _uow.SaveChangesAsync(false);
                await _uow.GetRepository<sys_trigger, string>().AddAsync(trigger);
                await _uow.SaveChangesAsync(false);
                await _uow.GetRepository<sys_jobtask_trigger, string>().AddAsync(jobtasktrigger);
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
        private sys_trigger GetTriggerEntity(ScheduleRequest scheduleModel) => scheduleModel switch
        {
            CronScheduleRequest cron => CronTrigger(cron),
            SimpleScheduleRequest simple => SimpleTrigger(simple),
            _ => throw new ArgumentNullException(nameof(scheduleModel)),
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cron"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private sys_trigger CronTrigger(CronScheduleRequest cron)
        {
            return new sys_trigger()
            {
                Id = Guid.NewGuid().ToString("N"),
                createtime = DateTime.UtcNow,
                description = cron.Trigger.Description,
                endat = cron.Trigger.EndAt,
                startat = cron.Trigger.StartAt,
                name = cron.Trigger.Name,
                priority = cron.Trigger.Priority,
                startnow = cron.Trigger.StartNow,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="simple"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private sys_trigger SimpleTrigger(SimpleScheduleRequest simple)
        {
            throw new NotImplementedException();
        }
    }
}
