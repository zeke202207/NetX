using NetX.Common;
using NetX.Common.Attributes;

namespace NetX.RBAC.Domain;

/// <summary>
/// 内置密码生成策略
/// </summary>
[Scoped]
public class BuildInPasswordStrategy : IPasswordStrategy
{
    private readonly IEncryption _encryption;

    public BuildInPasswordStrategy(IEncryption encryption)
    {
        _encryption = encryption;
    }

    public async Task<string> GeneratePassword()
    {
        return _encryption.Encryption(RBACConst.C_RBAC_BUILDIN_PASSWORD);
    }
}
