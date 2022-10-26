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
}
