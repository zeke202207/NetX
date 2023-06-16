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
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; set; }

        public ScheduleRequest Request { get; set; }

        public AddJobTaskCommand(
            string id,
            string name,
            string group,
            string jobtype,
            string datamap,
            bool disallowconcurrentexecution,
            DateTime createtime,
            string description,
            ScheduleRequest request)
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
            Request = request;
        }
    }
}
