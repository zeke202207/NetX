using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class DeptRequestModel
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("parentid")]
    public string? ParentId { get;set; }

    [JsonPropertyName("deptname")]
    public string DeptName { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("orderno")]
    public int OrderNo { get; set; }

    [JsonPropertyName("remark")]
    public string? Remark { get; set; }
}
