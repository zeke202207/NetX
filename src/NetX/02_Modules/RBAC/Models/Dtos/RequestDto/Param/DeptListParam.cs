using Newtonsoft.Json;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class DeptListParam
{
    /// <summary>
    /// 是否包含禁用部门
    /// </summary>
    [JsonProperty("containdisabled")]
    public bool ContainDisabled { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    [JsonProperty("deptname")]
    public string? DeptName { get; set; }

    /// <summary>
    /// 启用禁用状态
    /// </summary>
    [JsonProperty("status")]
    public string? Status { get; set; }
}
