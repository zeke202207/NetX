using NetX.Ddd.Domain;

namespace NetX.RBAC.Domain;

public record RoleAddCommand : DomainCommand
{
    public string RoleName { get; set; }
    public string Status { get; set; }
    public string ApiCheck { get; set; }
    public string? Remark { get; set; }
    public IEnumerable<string> Menus { get; set; }

    public RoleAddCommand(string roleName, string status, string apiCheck, string remark, IEnumerable<string> menus)
        : base(Guid.NewGuid(), DateTime.Now)
    {
        RoleName = roleName;
        Status = status;
        ApiCheck = apiCheck;
        Remark = remark;
        Menus = menus;
    }
}

public record RoleModifyCommand : DomainCommand
{
    public string Id { get; set; }
    public string RoleName { get; set; }
    public string Status { get; set; }
    public string ApiCheck { get; set; }
    public string? Remark { get; set; }
    public IEnumerable<string> Menus { get; set; }

    public RoleModifyCommand(string id, string roleName, string status, string apiCheck, string remark, IEnumerable<string> menus)
        : base(Guid.NewGuid(), DateTime.Now)
    {
        Id = id;
        RoleName = roleName;
        Status = status;
        ApiCheck = apiCheck;
        Remark = remark;
        Menus = menus;
    }
}


public record RoleRemoveCommand : DomainCommand
{
    public string Id { get; set; }

    public RoleRemoveCommand(string id)
        : base(Guid.NewGuid(), DateTime.Now)
    {
        Id = id;
    }
}

public record RoleStatusModifyCommand : DomainCommand
{
    public string Id { get; set; }
    public string Status { get; set; }

    public RoleStatusModifyCommand(string id, string status)
        : base(Guid.NewGuid(), DateTime.Now)
    {
        Id = id;
        Status = status;
    }
}

public record RoleApiAuthModifyCommand : DomainCommand
{
    public string Id { get; set; }
    public string Status { get; set; }

    public RoleApiAuthModifyCommand(string id, string status)
        : base(Guid.NewGuid(), DateTime.Now)
    {
        Id = id;
        Status = status;
    }
}

public record RoleApiAuthSettingCommand : DomainCommand
{
    public string RoleId { get; set; }
    public IEnumerable<string> ApiIds { get; set; }

    public RoleApiAuthSettingCommand(string roleId, IEnumerable<string> apiIds)
        : base(Guid.NewGuid(), DateTime.Now)
    {
        RoleId = roleId;
        ApiIds = apiIds;
    }
}