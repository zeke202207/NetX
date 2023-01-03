using Netx.Ddd.Domain;
using NetX.Common.Attributes;
using NetX.RBAC.Domain.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain;

[Scoped]
public class TestQueryHandler : DomainQueryHandler<TestQuery, string>
{

    public TestQueryHandler(IDatabaseContext dbContext)
        :base(dbContext)
    {
        
    }

    public override async Task<string> Handle(TestQuery request, CancellationToken cancellationToken)
    {
        var result = base._dbContext.QuerySingle<TestEntity>("select * from testentity where id=@id", new { id = "8464cb0a6e784a3cbb37e77828631966" });
        return await Task.FromResult("ok");
    }
}
