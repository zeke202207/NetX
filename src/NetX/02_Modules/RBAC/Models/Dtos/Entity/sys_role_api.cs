using FreeSql.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class sys_role_api
{
    /// <summary>
    /// 
    /// </summary>
    [Column(IsPrimary = true)]
    public string roleid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Column(IsPrimary = true)]
    public string apiid { get; set; }
}
