using Netx.Ddd.Domain;
using Newtonsoft.Json;

namespace NetX.RBAC.Domain;

public record AccountAddCommand : DomainCommand
{
    public string UserName { get; set; }
    public string NickName { get; set; }
    public string RoleId { get; set; }
    public string DeptId { get; set; }
    public string? Email { get; set; }
    public string? Remark { get; set; }

    public AccountAddCommand(string userName, string nickName, string roleId, string deptId, string email, string remark)
        : base(Guid.NewGuid(), DateTime.Now)
    {
        UserName= userName;
        NickName= nickName;
        RoleId= roleId;
        DeptId= deptId;
        Email= email;
        Remark= remark;
    }
}

public record AccountEditCommand : AccountAddCommand
{
    public string Id { get; set; }

    public AccountEditCommand(string id, string userName, string nickName, string roleId, string deptId, string email, string remark)
        : base(userName, nickName, roleId, deptId, email, remark)
    {
        Id = id;
    }
}

public record AccountModifyPwdCommand: DomainCommand
{
    public string Id { get; set; }
    public string OldPwd { get; set; }
    public string NewPwd { get; set; }

    public AccountModifyPwdCommand(string id, string oldpwd, string newPwd)
       : base(Guid.NewGuid(), DateTime.Now)
    {
        Id = id;
        OldPwd = oldpwd;
        NewPwd = newPwd;
    }
}

public record AccountRemoveCommand : DomainCommand
{
    public string Id { get; set; }

    public AccountRemoveCommand(string id)
        : base(Guid.NewGuid(), DateTime.Now)
    {
        Id = id;
    }
}