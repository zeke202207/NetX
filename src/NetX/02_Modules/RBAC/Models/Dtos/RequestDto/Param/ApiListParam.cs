using NetX.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
