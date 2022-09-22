using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class RoleListParam : Pager
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("roleName")]
    public string? RoleName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
