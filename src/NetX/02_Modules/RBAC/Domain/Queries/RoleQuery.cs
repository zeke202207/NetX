using Netx.Ddd.Domain;
using NetX.Common.ModuleInfrastructure;

namespace NetX.RBAC.Domain;

public class RolePagerListQuery : DomainQuery<ResultModel>
{
    public string? RoleName { get; set; }
    public string? Status { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

    public RolePagerListQuery(string roleName, string status, int currentPage, int pageSize)
    {
        RoleName = roleName;
        Status = status;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
}

public class RoleApiQuery : DomainQuery<ResultModel>
{
    public string Id { get; set; }

    public RoleApiQuery(string id)
    {
        Id = id;
    }
}
