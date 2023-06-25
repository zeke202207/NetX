using Netx.Ddd.Domain;
using NetX.TaskScheduling.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Domain
{
    public record AddJobTaskCommand : DomainCommand
    {
        public string Id { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Group { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string JobType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DataMap { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool DisAllowConcurrentExecution { get; set; }
        /// <summary>
        /// 任务是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 任务运行状态
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        public CronScheduleRequest Request { get; set; }

        public AddJobTaskCommand(
            string id,
            string name,
            string group,
            string jobtype,
            string datamap,
            bool disallowconcurrentexecution,
            DateTime createtime,
            string description,
            bool enabled,
            int state,
            CronScheduleRequest request)
       : base(Guid.NewGuid(), DateTime.Now)
        {
            Id = id;
            Name = name;
            Group = group;
            JobType = jobtype;
            DataMap = datamap;
            DisAllowConcurrentExecution = disallowconcurrentexecution;
            CreateTime = createtime;
            Description = description;
            Enabled = enabled;
            State = state;
            Request = request;
        }
    }
}
