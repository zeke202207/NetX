using NetX.Ddd.Domain;
using NetX.Ddd.Domain.Aggregates;
using System.ComponentModel.DataAnnotations.Schema;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
[UPKey("roleid", "apiid")]
public class sys_role_api : BaseEntity<string>
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
    public string apiid { get; set; }
}
