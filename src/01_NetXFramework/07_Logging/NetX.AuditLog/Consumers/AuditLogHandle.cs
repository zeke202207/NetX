using Microsoft.Extensions.Logging;
using NetX.MemoryQueue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.AuditLog
{
    /// <summary>
    /// 审计日志处理器
    /// </summary>
    public class AuditLogHandle : IConsumer<AuditLogConsumerModel>
    {
        private readonly IAuditLogStoreProvider _auditLogStore;
        private readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="auditLogStore"></param>
        /// <param name="log"></param>
        public AuditLogHandle(IAuditLogStoreProvider auditLogStore, ILogger<AuditLogHandle> log)
        {
            _auditLogStore = auditLogStore;
            _logger = log;
        }

        public string QueueName => NetxConstEnum.C_QUEUE_AUDITLOG_HANDLE;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task Handle(AuditLogConsumerModel message)
        {
            try
            {
                if (null == _auditLogStore)
                    return;
                await _auditLogStore.SaveAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError("审计日志保存失败", ex);
            }
        }
    }
}
