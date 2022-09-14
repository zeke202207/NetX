using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class UserListModel :UserModel
{
    [JsonPropertyName("roleid")]
    public string RoleId { get; set; }

    [JsonPropertyName("rolename")]
    public string RoleName { get; set; }

    [JsonPropertyName("deptid")]
    public string DeptId { get; set; }

    [JsonPropertyName("deptname")]
    public string DeptName { get;set; }
}
