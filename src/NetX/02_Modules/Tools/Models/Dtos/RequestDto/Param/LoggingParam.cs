using NetX.Common.Models;
using Newtonsoft.Json;

namespace NetX.Tools.Models;

/// <summary>
/// 
/// </summary>
public class LoggingParam : Pager
{
    /// <summary>
    /// 日志等级
    /// </summary>
    [JsonProperty("level")]
    public int Level { get; set; }
}
