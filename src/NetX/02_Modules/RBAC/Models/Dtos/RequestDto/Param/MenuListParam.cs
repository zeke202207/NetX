using NetX.Common.Models;
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
    [JsonProperty("menuName")]
    public string? MenuName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("status")]
    public string? Status { get; set; }
}
