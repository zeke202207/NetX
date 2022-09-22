using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class RoleModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("rolename")]
    public string RoleName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("createtime")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("menu")]
    public List<string> Menus { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("remark")]
    public string Remark { get; set; }
}
