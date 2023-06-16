using Netx.Ddd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.TaskScheduling.Domain
{
    public record RemoveJobTaskCommand : DomainCommand
    {
        public string Id { get; set; }

        public RemoveJobTaskCommand(string id)
            : base(Guid.NewGuid(), DateTime.Now)
        {
            Id = id;
        }
    }
}
