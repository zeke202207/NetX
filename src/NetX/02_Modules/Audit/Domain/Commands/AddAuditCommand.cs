using NetX.AuditLog;
using NetX.Ddd.Domain;

namespace NetX.Audit.Domain.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public record AddAuditCommand : DomainCommand
    {
        public AuditLogConsumerModel Info { get; private set; }

        public AddAuditCommand(AuditLogConsumerModel info)
            : base(Guid.NewGuid(), DateTime.Now)
        {
            Info = info;
        }
    }
}
