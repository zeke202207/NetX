using FreeSql.DataAnnotations;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class sys_user_role
{
    /// <summary>
    /// 
    /// </summary>
    [Column(IsPrimary = true)]
    public string userid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [Column(IsPrimary = true)]
    public string roleid { get; set; }
}
