using NetX.Ddd.Domain;
using NetX.Common.ModuleInfrastructure;

namespace NetX.RBAC.Domain;

public class DeptPagerListQuery : DomainQuery<ResultModel>
{

    public bool ContainDisabled { get; set; }
    public string? DeptName { get; set; }
    public string? Status { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

    public DeptPagerListQuery(bool containDisabled, string deptName, string status, int currentPage, int pageSize)
    {
        ContainDisabled = containDisabled;
        DeptName = deptName;
        Status = status;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
}

