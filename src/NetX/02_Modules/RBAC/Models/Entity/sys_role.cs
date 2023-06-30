using Netx.Ddd.Domain.Aggregates;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class sys_role : BaseEntity<string>
{
    /// <summary>
    /// 
    /// </summary>
    public string rolename { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int apicheck { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime createtime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string remark { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool issystem { get; set; }
}
