using Newtonsoft.Json;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class MenuListParam : Pager
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("menuName")]
    public string? MenuName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("status")]
    public string? Status { get; set; }
}
