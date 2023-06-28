using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.AuditLog
{
    public class DefaultAuditLogStore : IAuditLogStoreProvider
    {
        public async Task SaveAsync(AuditLogConsumerModel model)
        {
            await Task.CompletedTask;
        }
    }
}
