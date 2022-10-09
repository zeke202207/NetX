using Newtonsoft.Json;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class AccountRequestModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("id")]
    public string? Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("roleid")]
    public string? RoleId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("deptid")]
    public string? DeptId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("username")]
    public string UserName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("nickname")]
    public string NickName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("email")]
    public string? Email { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }
}
