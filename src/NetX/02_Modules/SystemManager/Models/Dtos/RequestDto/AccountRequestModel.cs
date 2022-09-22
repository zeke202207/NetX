using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class AccountRequestModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("roleid")]
    public string? RoleId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("deptid")]
    public string? DeptId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("nickname")]
    public string NickName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("remark")]
    public string? Remark { get; set; }
}
