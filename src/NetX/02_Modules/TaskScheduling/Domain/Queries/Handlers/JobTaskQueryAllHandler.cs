using Netx.Ddd.Domain;
using NetX.Common.Attributes;
using NetX.TaskScheduling.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Domain
{
    [Scoped]
    public class JobTaskQueryAllHandler : DomainQueryHandler<JobTaskQueryAll, IEnumerable<JobTaskModel>>
    {
        public JobTaskQueryAllHandler(IDatabaseContext dbContext)
                   : base(dbContext)
        {

        }

        public override async Task<IEnumerable<JobTaskModel>> Handle(JobTaskQueryAll request, CancellationToken cancellationToken)
        {
            var jobtask = await base._dbContext.QueryListAsync<sys_jobtask>(@$"SELECT * FROM sys_jobtask WHERE enabled=0", null);
            List<JobTaskModel> results = new List<JobTaskModel>();
            foreach (var item in jobtask)
            {
                var jobtaskModel = new JobTaskModel()
                {
                    Id = item.Id,
                    Name = item.name,
                    Description = item.description,
                    DisAllowConcurrentExecution = item.disallowconcurrentexecution,
                    Group = item.group,
                    JobDataMap = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string,string>>(item.datamap.Replace(@"\","")),
                    JobType = item.jobtype
                };
                var trigger = await base._dbContext.QuerySingleAsync<sys_jobtasktrigger>(@"SELECT * FROM sys_jobtasktrigger WHERE jobtaskid =@jobtaskid", new { jobtaskid = item.Id });
                if (null != trigger)
                    jobtaskModel.Trigger = GetTriggerModel(trigger);
                results.Add(jobtaskModel);
            }
            return results;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private CronJobTaskTriggerModel GetTriggerModel(sys_jobtasktrigger trigger)
        {
            return new CronJobTaskTriggerModel()
            {
                CronExpression = trigger.cron,
                Description = trigger.description,
                StartAt = trigger.startat,
                EndAt = trigger.endat,
                Priority = trigger.priority,
                Name = trigger.name,
                StartNow = trigger.startnow
            };
        }
    }
}
