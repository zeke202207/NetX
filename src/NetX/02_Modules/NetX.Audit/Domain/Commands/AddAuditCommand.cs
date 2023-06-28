using Netx.Ddd.Domain;
using NetX.AuditLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Audit.Domain.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public record AddAuditCommand : DomainCommand
    {
        public AuditLogConsumerModel Info { get; private set; }

        public AddAuditCommand(AuditLogConsumerModel info)
            :base(Guid.NewGuid(), DateTime.Now)
        {
            Info = info;
        }
    }
}
