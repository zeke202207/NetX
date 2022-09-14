using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class RoleListParam :Pager
{
    [JsonPropertyName("roleName")]
    public string? RoleName { get; set; }
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
