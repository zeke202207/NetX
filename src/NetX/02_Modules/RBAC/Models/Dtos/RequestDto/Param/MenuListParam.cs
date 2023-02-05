using NetX.Common.ModuleInfrastructure;
using Newtonsoft.Json;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class MenuListParam : Pager
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("menuname")]
    public string? Title { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("status")]
    public string? Status { get; set; }
}
