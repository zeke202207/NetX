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
public class RoleApiAuthModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("id")]
    public string RoleId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("apiids")]
    public IEnumerable<string> ApiIds { get; set; }
}
