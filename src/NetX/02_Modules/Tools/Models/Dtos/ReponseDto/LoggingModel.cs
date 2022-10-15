using Newtonsoft.Json;

namespace NetX.Tools.Models;

/// <summary>
/// 
/// </summary>
public class LoggingModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("level")]
    public int Level { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("eventid")]
    public string EventId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("exception")]
    public string Exception { get; set; }

    ///// <summary>
    ///// 
    ///// </summary>
    //[JsonProperty("context")]
    //public string Context { get; set; }

    ///// <summary>
    ///// 
    ///// </summary>
    //[JsonProperty("state")]
    //public string State { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("threadid")]
    public string ThreadId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("createtime")]
    public DateTime CreateTime { get; set; }
}
