using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.LogCollector.Models;

/// <summary>
/// 
/// </summary>
public class sys_audit_logging : BaseEntity
{
    /// <summary>
    /// 
    /// </summary>
    public string userid { get;set; }
    /// <summary>
    /// 
    /// </summary>
    public string nickname { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string operation { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string requestparams { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string ip { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string response { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string influencedata { get; set; }
}
