using NetX.Ddd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Domain.Commands
{
    public record EnabledJobCommand : DomainCommand
    {
        public string Id { get; set; }

        public bool Enabled { get; set; }

        public EnabledJobCommand(string id, bool enabled)
        {
            Id = id;
            Enabled = enabled;
        }
    }
}
