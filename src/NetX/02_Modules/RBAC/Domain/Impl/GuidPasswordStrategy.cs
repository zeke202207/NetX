using NetX.Common.Attributes;

namespace NetX.RBAC.Domain;

[Scoped]
public class GuidPasswordStrategy : IPasswordStrategy
{
    public async Task<string> GeneratePassword()
    {
        return "netx";
        //return await Task.FromResult(Guid.NewGuid().ToString("N"));
    }
}
