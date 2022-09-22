using FreeSql.DataAnnotations;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class sys_user_dept
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
    public string deptid { get; set; }
}
