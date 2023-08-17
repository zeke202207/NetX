using Microsoft.Extensions.DependencyInjection;
using NetX.Audit.Domain.Commands;
using NetX.AuditLog;
using NetX.Ddd.Core;
using NetX.Tenants.Extensions;

namespace NetX.Audit.Provider
{
    public class DatabaseStoreProvider : IAuditLogStoreProvider
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditCommand"></param>
        public DatabaseStoreProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// 保存审计日志到数据库
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task SaveAsync(AuditLogConsumerModel model)
        {
            _serviceProvider.MockTenantScope(model.TenandId, (scope, tenant) =>
               {
                   ICommandBus _auditCommand = scope.ServiceProvider.GetService<ICommandBus>();
                   _auditCommand.Send<AddAuditCommand>(new AddAuditCommand(model)).GetAwaiter().GetResult();
               });
        }
    }
}
