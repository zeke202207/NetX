using Netx.Ddd.Domain.Aggregates;

namespace NetX.RBAC.Domain.Models.Entities;

public class TestEntity : BaseEntity<string>
{
    public string Name { get; set; }
}


public class TestEntity1 : BaseEntity<string>
{
    public string Name1 { get; set; }
}

