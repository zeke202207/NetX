using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 登录实体对象
/// </summary>
public class LoginModel
{
    /// <summary>
    /// 用户名
    /// </summary>
    [JsonPropertyName("username")]
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [JsonPropertyName("password")]
    public string Password { get; set; }
}
