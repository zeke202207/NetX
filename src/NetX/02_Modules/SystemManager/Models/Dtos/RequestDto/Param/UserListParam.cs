using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class UserListParam :Pager
{
    [JsonPropertyName("deptId")]
    public string? DeptId { get; set; }
    [JsonPropertyName("account")]
    public string? Account { get; set; }
    [JsonPropertyName("nickname")]
    public string? Nickname { get; set; }
}
