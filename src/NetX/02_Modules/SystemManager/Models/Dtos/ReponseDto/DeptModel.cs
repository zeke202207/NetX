using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class DeptModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("parentId")]
    public string ParentId { get; set; }
    [JsonPropertyName("deptName")]
    public string DeptName { get; set; }
    [JsonPropertyName("orderNo")]
    public int OrderNo { get; set; }
    [JsonPropertyName("createTime")]
    public DateTime CreateTime { get; set; }
    [JsonPropertyName("remark")]
    public string Remark { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("children")]
    public List<DeptModel> Children { get; set; }
}
