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
