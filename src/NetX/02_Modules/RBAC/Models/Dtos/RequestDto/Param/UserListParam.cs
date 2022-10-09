using Newtonsoft.Json;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class UserListParam : Pager
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("deptId")]
    public string? DeptId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("account")]
    public string? Account { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("nickname")]
    public string? Nickname { get; set; }
}
