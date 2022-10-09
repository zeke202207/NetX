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
}
