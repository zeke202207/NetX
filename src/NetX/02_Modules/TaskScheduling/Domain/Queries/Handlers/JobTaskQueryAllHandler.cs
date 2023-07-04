using Dapper;
using Netx.Ddd.Domain;
using NetX.Common.Attributes;
using NetX.TaskScheduling.Model;
using Newtonsoft.Json;
using Quartz.Util;
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
            string sql = @"SELECT 
    j.id as Id,
    j.name,
    j.group,
    j.jobtype,
    j.datamap,
    j.disallowconcurrentexecution,
    j.enabled,
    j.state,
    j.createtime,
    j.description,
    t.id  as triggerid,
    t.name as triggername,
    t.cron,
    t.triggertype,
    t.startat,
    t.endat,
    t.startnow,
    t.priority
FROM
    sys_jobtask j
        LEFT JOIN
    sys_jobtasktrigger t 
        ON 
    t.jobtaskid = j.id 
WHERE 1=1 ";
            var param = new DynamicParameters();
            if (request != null && !request.JobName.IsNullOrWhiteSpace())
            {
                sql += @" j.name LIKE CONCAT('%',@name,'%') ";
                param.Add("name", request.JobName);
            }
            sql += @" ORDER BY j.createtime DESC ";
            var jobdetails = await base._dbContext.QueryListAsync<jobinfo>(sql, param);
            return jobdetails?.Select(p => new JobTaskModel()
            {
                Id = p.Id,
                Description = p.description,
                DisAllowConcurrentExecution = p.disallowconcurrentexecution,
                Enabled = Convert.ToBoolean(p.enabled),
                Group = p.group,
                JobDataMap = p.datamap.IsNullOrWhiteSpace()? new Dictionary<string, string>() : JsonConvert.DeserializeObject<Dictionary<string, string>>(p.datamap.Replace(@"\", "")),
                JobType = p.jobtype,
                Name = p.name,
                State = (JobTaskState)p.state,
                Trigger = new CronJobTaskTriggerModel()
                {
                    Name = p.triggername,
                    CronExpression = p.cron,
                    Description = p.description,
                    EndAt = p.endat,
                    Priority = p.priority,
                    StartAt = p.startat,
                    StartNow = p.startnow,
                }
            });
        }

        private class jobinfo : sys_jobtask
        {
            public string triggerid { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public string triggername { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public DateTime? startat { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public DateTime? endat { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public bool startnow { get; set; }
            /// <summary>
            /// 
            /// </summary>
            public int priority { get; set; }

            public string cron { get; set; }

            /// <summary>
            /// 触发器类型
            /// 0：cron
            /// 1：simple
            /// 2：。。。
            /// </summary>
            public int triggertype { get; set; }
        }
    }
}
