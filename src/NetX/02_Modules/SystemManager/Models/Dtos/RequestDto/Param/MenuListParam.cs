using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class MenuListParam : Pager
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("menuName")]
    public string? MenuName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
