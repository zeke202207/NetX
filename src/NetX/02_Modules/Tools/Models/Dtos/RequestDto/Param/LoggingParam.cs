using NetX.Common.Models;
using Newtonsoft.Json;

namespace NetX.Tools.Models;

/// <summary>
/// 
/// </summary>
public class LoggingParam : Pager
{
    [JsonProperty("level")]
    public int Level { get; set; }
}
