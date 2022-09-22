using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class DeptModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("parentid")]
    public string ParentId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("deptname")]
    public string DeptName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("orderno")]
    public int OrderNo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("createtime")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("remark")]
    public string Remark { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("children")]
    public List<DeptModel> Children { get; set; }
}
