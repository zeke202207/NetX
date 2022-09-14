using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class UserModel
{
    [JsonPropertyName("userid")]
    public string UserId { get; set; }
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    [JsonIgnore]
    public string Password { get; set; }
    [JsonPropertyName("nickname")]
    public string NickName { get;set; }
    [JsonPropertyName("avatar")]
    public string Avatar { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("desc")]
    public string Remark { get; set; }
    //public string HomePath { get; set; }
    //
}
