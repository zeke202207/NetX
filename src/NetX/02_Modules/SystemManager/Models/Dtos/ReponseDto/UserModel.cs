using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class UserModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string Password { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("nickname")]
    public string NickName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("avatar")]
    public string Avatar { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("remark")]
    public string Remark { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("email")]
    public string Email { get; set; }
    //public string HomePath { get; set; }
}
