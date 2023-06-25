using Netx.Ddd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public record SchedulerListenerCommand : DomainCommand
    {
        public string Name { get; set; }

        public string Group { get; set; }

        public JobTaskState State { get; set; }

        public SchedulerListenerCommand(string name, string group , JobTaskState state) 
            : base(Guid.NewGuid(), DateTime.Now)
        {
            Name = name;
            Group = group;  
            State = state;
        }
    }
}
