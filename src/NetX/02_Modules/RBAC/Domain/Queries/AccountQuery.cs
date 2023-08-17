using NetX.Common.ModuleInfrastructure;
using NetX.Ddd.Domain;
using System.Diagnostics.CodeAnalysis;

namespace NetX.RBAC.Domain.Queries;

public class LoginQuery : DomainQuery<ResultModel>
{
    public string UserName { get; private set; }
    public string Password { get; private set; }

    public LoginQuery([NotNull] string username, [NotNull] string password)
    {
        UserName = username;
        Password = password;
    }
}

public class LoginUserInfoQuery : DomainQuery<ResultModel>
{
    public string UserId { get; private set; }

    public LoginUserInfoQuery([NotNull] string userId)
    {
        UserId = userId;
    }
}

public class AccountListQuery : DomainQuery<ResultModel>
{
    public string DeptId { get; set; }
    public string Account { get; set; }
    public string NickName { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }

    public AccountListQuery(string deptId, string account, string nickName, int currentPage, int pageSize)
    {
        DeptId = deptId;
        Account = account;
        NickName = nickName;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }
}

public class AccountPermCodeQuery : DomainQuery<ResultModel>
{
    public string UserId { get; set; }

    public AccountPermCodeQuery([NotNull] string currentUserId)
    {
        UserId = currentUserId;
    }
}