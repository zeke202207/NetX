using Newtonsoft.Json;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class UserModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("username")]
    public string UserName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonIgnore]
    public string Password { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("nickname")]
    public string NickName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("avatar")]
    public string Avatar { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("remark")]
    public string Remark { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("email")]
    public string Email { get; set; }
    //public string HomePath { get; set; }
}
