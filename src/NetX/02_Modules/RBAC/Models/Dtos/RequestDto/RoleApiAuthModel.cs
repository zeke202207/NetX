using Newtonsoft.Json;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class RoleApiAuthModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("id")]
    public string RoleId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("apiids")]
    public IEnumerable<string> ApiIds { get; set; }
}
