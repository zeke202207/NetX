using NetX.Common.ModuleInfrastructure;
using Netx.Ddd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain;

public class ApiPagerListQuery : DomainQuery<ResultModel>
{
    public string Group { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

    public ApiPagerListQuery(string group, int currentPage, int pageSize)
    {
        Group = group;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
}

public class ApiListQuery : DomainQuery<ResultModel>
{
    public ApiListQuery()
    {
    }
}
