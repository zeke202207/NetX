using NetX.Ddd.Domain;

namespace NetX.RBAC.Domain;

public record ApiAddCommand : DomainCommand
{
    public string Path { get; set; }
    public string Group { get; set; }
    public string Method { get; set; }
    public string? Description { get; set; }

    public ApiAddCommand(string path, string group, string method, string description)
        : base(Guid.NewGuid(), DateTime.Now)
    {
        Path = path;
        Group = group;
        Method = method;
        Description = description;
    }
}

public record ApiModifyCommand : DomainCommand
{
    public string Id { get; set; }
    public string Path { get; set; }
    public string Group { get; set; }
    public string Method { get; set; }
    public string? Description { get; set; }

    public ApiModifyCommand(string id, string path, string group, string method, string description)
        : base(Guid.NewGuid(), DateTime.Now)
    {
        Id = id;
        Path = path;
        Group = group;
        Method = method;
        Description = description;
    }
}

public record ApiRemoveCommand : DomainCommand
{
    public string Id { get; set; }

    public ApiRemoveCommand(string id)
        : base(Guid.NewGuid(), DateTime.Now)
    {
        Id = id;
    }
}
