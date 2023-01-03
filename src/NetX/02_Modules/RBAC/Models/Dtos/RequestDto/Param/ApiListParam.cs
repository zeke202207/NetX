using NetX.Common.ModuleInfrastructure;
using Newtonsoft.Json;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class ApiPageParam : Pager
{
    [JsonProperty("group")]
    public string? Ggroup { get; set; }
}

/// <summary>
/// 
/// </summary>
public class ApiParam
{

}
