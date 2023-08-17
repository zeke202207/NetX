namespace NetX.RBAC.Domain.Commands
{
    public interface IRoleManager
    {
        Task<bool> RemovePermissionCacheAsync(string roleId);
    }
}
