using Netx.Ddd.Domain;
using NetX.Common.ModuleInfrastructure;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain.Queries;

public class LoginQuery : DomainQuery<ResultModel>
{
    public string UserName { get; private set; }
    public string Password { get; private set; }

    public LoginQuery([NotNull]string username, [NotNull] string password)
    {
        UserName= username;
        Password= password;
    }
}

public class LoginUserInfoQuery : DomainQuery<ResultModel>
{
    public string UserId { get; private set; }

    public LoginUserInfoQuery([NotNull]string userId)
    {
        UserId = userId;
    }
}