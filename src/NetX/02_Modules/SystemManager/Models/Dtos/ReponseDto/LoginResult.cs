using Newtonsoft.Json;

namespace NetX.SystemManager.Models;

/// <summary>
/// 登录返回结果实体对象
/// </summary>
public class LoginResult
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("userid")]
    public string UserId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("username")]
    public string UserName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("token")]
    public string Token { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("realname")]
    public string RealName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("desc")]
    public string Desc { get; set; }
    //public roles,
}
