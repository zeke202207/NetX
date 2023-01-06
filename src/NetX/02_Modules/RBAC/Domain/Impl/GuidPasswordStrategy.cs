using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain;

[Scoped]
public class GuidPasswordStrategy : IPasswordStrategy
{
    public async Task<string> GeneratePassword()
    {
        return await Task.FromResult(Guid.NewGuid().ToString("N"));
    }
}
