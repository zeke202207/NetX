namespace NetX.RBAC.Domain.Commands
{
    public interface IApiManager
    {
        Task<bool> RemovePermissionCacheAsync(string apiid);
    }
}
