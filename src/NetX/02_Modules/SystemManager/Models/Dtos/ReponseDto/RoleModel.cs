using Newtonsoft.Json;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class RoleModel
{
    [JsonProperty("id")]
    public string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("rolename")]
    public string RoleName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("apicheck")]
    public string ApiCheck { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("createtime")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("menu")]
    public List<string> Menus { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("remark")]
    public string Remark { get; set; }
}
