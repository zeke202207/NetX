using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.Models;

/// <summary>
/// 
/// </summary>
public class sys_login_logging : BaseEntity
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
    public string loginip { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string loginaddress { get; set; }
}
