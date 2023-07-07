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

public class RoleByIdQuery : DomainQuery<ResultModel>
{
    public string Id { get; set; }

    public RoleByIdQuery(string id)
    {
        Id = id;
    }
}

public class RoleApiQuery : DomainQuery<ResultModel>
{
    public string RoleId { get; set; }

    public RoleApiQuery(string roleId)
    {
        RoleId = roleId;
    }
}

public class RoleApiIdsQuery : DomainQuery<ResultModel>
{
    public string RoleId { get; set; }

    public RoleApiIdsQuery(string roleId)
    {
        RoleId = roleId;
    }
}
