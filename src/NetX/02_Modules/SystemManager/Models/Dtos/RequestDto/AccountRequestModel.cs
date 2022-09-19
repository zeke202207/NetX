using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class AccountRequestModel
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("roleid")]
    public string? RoleId { get; set; }
    [JsonPropertyName("deptid")]
    public string? DeptId { get; set; }
    [JsonPropertyName("username")]
    public string UserName { get; set; }
    [JsonPropertyName("nickname")]
    public string NickName { get; set; }
    [JsonPropertyName("email")]
    public string? Email { get; set; }
    [JsonPropertyName("remark")]
    public string? Remark { get; set; }
}
