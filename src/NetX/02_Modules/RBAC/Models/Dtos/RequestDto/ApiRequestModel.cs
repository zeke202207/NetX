using Newtonsoft.Json;

namespace NetX.RBAC.Models;

public class ApiRequestModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("id")]
    public string? Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("path")]
    public string Path { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("group")]
    public string Group { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("method")]
    public string Method { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("description")]
    public string? Description { get; set; }
}
