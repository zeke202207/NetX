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
