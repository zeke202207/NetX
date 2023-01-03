using Netx.Ddd.Domain;
using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain;

[Scoped]
public class TestQueryHandler : DomainQueryHandler<TestQuery, string>
{
    public override async Task<string> Handle(TestQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult("ok");
    }
}
