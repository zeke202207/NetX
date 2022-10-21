using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.Models;

/// <summary>
/// 
/// </summary>
public class AuditLoggingModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("userid")]
    public string UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("username")]
    public string UserName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("controller")]
    public string Controller { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("action1")]
    public string Action { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("remoteipv4")]
    public string RemoteIpv4 { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("httpmethod")]
    public string HttpMethod { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("detail")]
    public string Detail { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("createtime")]
    public DateTime CreateTime { get; set; }
}
