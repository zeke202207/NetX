using Netx.Ddd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Domain
{
    public record StateJobCommand : DomainCommand
    {
        public string Id { get; set; }

        public int State { get; set; }

        public StateJobCommand(string id, int state)
        {
            Id = id;
            State = state;
        }
    }
}
