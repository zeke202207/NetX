using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.Models;

/// <summary>
/// 
/// </summary>
public class sys_audit_logging : BaseEntity
{
    /// <summary>
    /// 
    /// </summary>
    public string userid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string username { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string controller { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string action { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string remoteipv4 { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string httpmethod { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string detail { get; set; }
}
