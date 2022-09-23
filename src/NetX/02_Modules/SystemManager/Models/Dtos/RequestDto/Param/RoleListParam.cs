using Newtonsoft.Json;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class RoleListParam : Pager
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("roleName")]
    public string? RoleName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("status")]
    public string? Status { get; set; }
}
