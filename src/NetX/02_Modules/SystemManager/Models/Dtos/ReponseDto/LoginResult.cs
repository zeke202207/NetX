using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 登录返回结果实体对象
/// </summary>
public class LoginResult
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("userid")]
    public string UserId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("token")]
    public string Token { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("realname")]
    public string RealName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("desc")]
    public string Desc { get; set; }
    //public roles,
}
