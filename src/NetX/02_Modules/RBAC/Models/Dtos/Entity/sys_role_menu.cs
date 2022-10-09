using FreeSql.DataAnnotations;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class sys_role_menu
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
    public string menuid { get; set; }
}
