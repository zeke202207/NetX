using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class MenuListParam : Pager
{
    [JsonPropertyName("menuName")]
    public string? MenuName { get; set; }
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}
