using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

/// <summary>
/// 登录返回结果实体对象
/// </summary>
public class LoginResult
{
    [JsonPropertyName("userid")]
    public string UserId { get; set; }
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    [JsonPropertyName("token")]
    public string Token { get; set; }
    [JsonPropertyName("realname")]
    public string RealName { get; set; }
    [JsonPropertyName("desc")]
    public string Desc { get; set; }
    //public roles,
}
