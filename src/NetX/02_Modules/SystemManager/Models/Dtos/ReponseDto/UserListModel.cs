using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class UserListModel : UserModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("roleid")]
    public string? RoleId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("rolename")]
    public string? RoleName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("deptid")]
    public string? DeptId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("deptname")]
    public string? DeptName { get; set; }
}
