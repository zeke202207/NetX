using Newtonsoft.Json;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class DeptModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("parentid")]
    public string ParentId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("deptname")]
    public string DeptName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("orderno")]
    public int OrderNo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("createtime")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("remark")]
    public string Remark { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("children")]
    public List<DeptModel> Children { get; set; }
}
