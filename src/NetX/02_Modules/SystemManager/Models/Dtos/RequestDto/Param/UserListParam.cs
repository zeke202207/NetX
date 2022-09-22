using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class UserListParam : Pager
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("deptId")]
    public string? DeptId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("account")]
    public string? Account { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("nickname")]
    public string? Nickname { get; set; }
}
