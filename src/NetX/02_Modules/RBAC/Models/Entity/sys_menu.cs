using Netx.Ddd.Domain.Aggregates;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class sys_menu : BaseEntity<string>
{
    /// <summary>
    /// 
    /// </summary>
    public string parentid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string icon { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int orderno { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string permission { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string title { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string path { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string component { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? redirect { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int isext { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int keepalive { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public int show { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string meta { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime createtime { get; set; }
}
