using Netx.Ddd.Domain.Aggregates;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class sys_user : BaseEntity<string>
{
    /// <summary>
    /// 
    /// </summary>
    public string username { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string password { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string nickname { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string avatar { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? email { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? remark { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool issystem { get; set; }
}
