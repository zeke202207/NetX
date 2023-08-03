using NetX.Ddd.Domain;
using NetX.Ddd.Domain.Aggregates;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
[UPKey("roleid", "menuid")]
public class sys_role_menu : BaseEntity<string>
{
    [NotMapped]
    public new string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string roleid { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string menuid { get; set; }
}
