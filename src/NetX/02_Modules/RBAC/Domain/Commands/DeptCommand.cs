using Netx.Ddd.Domain;

namespace NetX.RBAC.Domain;

public record DeptAddCommand : DomainCommand
{
    public string? ParentId { get; set; }
    public string DeptName { get; set; }
    public string Status { get; set; }
    public int OrderNo { get; set; }
    public string? Remark { get; set; }

    public DeptAddCommand(string parentId, string deptName, string status, int orderNo, string remark)
    {
        ParentId = parentId;
        DeptName = deptName;
        Status = status;
        OrderNo = orderNo;
        Remark = remark;
    }
}

public record DeptModifyCommand : DomainCommand
{
    public string Id { get; set; }
    public string? ParentId { get; set; }
    public string DeptName { get; set; }
    public string Status { get; set; }
    public int OrderNo { get; set; }
    public string? Remark { get; set; }

    public DeptModifyCommand(string id, string parentId, string deptName, string status, int orderNo, string remark)
    {
        Id = id;
        ParentId = parentId;
        DeptName = deptName;
        Status = status;
        OrderNo = orderNo;
        Remark = remark;
    }
}

public record DeptRemoveCommand : DomainCommand
{
    public string Id { get; set; }

    public DeptRemoveCommand(string id)
        : base(Guid.NewGuid(), DateTime.Now)
    {
        Id = id;
    }
}