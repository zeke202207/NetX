using NetX.Ddd.Domain;
using NetX.Common.ModuleInfrastructure;

namespace NetX.RBAC.Domain;

public class MenuPagerListQuery : DomainQuery<ResultModel>
{
    public string? Title { get; set; }
    public string? Status { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

    public MenuPagerListQuery(string title, string status, int currentPage, int pageSize)
    {
        Title = title;
        Status = status;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
}

public class MenuCurrentUserQuery : DomainQuery<ResultModel>
{
    public string UserId { get; set; }

    public MenuCurrentUserQuery(string userId)
    {
        UserId = userId;
    }
}
