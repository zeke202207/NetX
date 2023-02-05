using Netx.Ddd.Domain;
using NetX.RBAC.Models;

namespace NetX.RBAC.Domain;

public record MenuAddCommand : DomainCommand
{
    public string? ParentId { get; set; }
    public string? Path { get; set; }
    public string? Title { get; set; }
    public string? Component { get; set; }
    public string? Redirect { get; set; }
    public MenuMetaData? Meta { get; set; }
    public string? Icon { get; set; }
    public string Type { get; set; }
    public string? Permission { get; set; }
    public int? OrderNo { get; set; }
    public string? Status { get; set; }
    public int IsExt { get; set; }
    public int KeepAlive { get; set; }
    public int Show { get; set; }
    public string? ExtPath { get; set; }

    public MenuAddCommand(
        string parentId,
        string path,
        string title,
        string component,
        string redirect,
        MenuMetaData meta,
        string icon,
        string type,
        string permission,
        int? orderNo,
        string status,
        int isExt,
        int show,
        string extPath)
    {
        ParentId = parentId;
        Path = path;
        Title = title;
        Component = component;
        Redirect = redirect;
        Meta = meta;
        Icon = icon;
        Type = type;
        Permission = permission;
        OrderNo = orderNo;
        Status = status;
        IsExt = isExt;
        Show = show;
        ExtPath = extPath;
    }
}

public record MenuModifyCommand : DomainCommand
{
    public string Id { get; set; }
    public string? ParentId { get; set; }
    public string? Path { get; set; }
    public string? Title { get; set; }
    public string? Component { get; set; }
    public string? Redirect { get; set; }
    public MenuMetaData? Meta { get; set; }
    public string? Icon { get; set; }
    public string Type { get; set; }
    public string? Permission { get; set; }
    public int? OrderNo { get; set; }
    public string? Status { get; set; }
    public int IsExt { get; set; }
    public int KeepAlive { get; set; }
    public int Show { get; set; }
    public string? ExtPath { get; set; }

    public MenuModifyCommand(
        string id,
        string parentId,
        string path,
        string title,
        string component,
        string redirect,
        MenuMetaData meta,
        string icon,
        string type,
        string permission,
        int? orderNo,
        string status,
        int isExt,
        int show,
        string extPath)
    {
        Id = id;
        ParentId = parentId;
        Path = path;
        Title = title;
        Component = component;
        Redirect = redirect;
        Meta = meta;
        Icon = icon;
        Type = type;
        Permission = permission;
        OrderNo = orderNo;
        Status = status;
        IsExt = isExt;
        Show = show;
        ExtPath = extPath;
    }
}

public record MenuRemoveCommand : DomainCommand
{
    public string Id { get; set; }

    public MenuRemoveCommand(string id)
        : base(Guid.NewGuid(), DateTime.Now)
    {
        Id = id;
    }
}
