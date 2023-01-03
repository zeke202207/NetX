using Netx.Ddd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain;

public class TestQuery :DomainQuery<string>
{
    public string Param { get; private set; }

    public TestQuery(string param)
    {
        Param = param;
    }
}
