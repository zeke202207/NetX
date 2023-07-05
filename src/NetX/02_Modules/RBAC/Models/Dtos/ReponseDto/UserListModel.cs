using Newtonsoft.Json;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class UserListModel : UserModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("roleid")]
    public string? RoleId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("rolename")]
    public string? RoleName { get; set; }

    [JsonProperty("rolestatus")]
    public string RoleStatus { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("deptid")]
    public string? DeptId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("deptname")]
    public string? DeptName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("issystem")]
    public bool IsSystem { get; set; }
}
