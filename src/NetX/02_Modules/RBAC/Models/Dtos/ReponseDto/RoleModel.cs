using Newtonsoft.Json;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class RoleModel
{
    /// <summary>
    /// 
    /// </summary>
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
    [JsonProperty("checkmenu")]
    public CheckMenu CheckMenu { get; set; }


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
