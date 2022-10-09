using Newtonsoft.Json;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class MenuMetaData
{
    [JsonProperty("title")]
    public string Title { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("hideMenu")]
    public bool HideMenu { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("ignoreKeepAlive")]
    public bool IgnoreKeepAlive { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("hideChildrenInMenu")]
    public bool HideChildrenMenu { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("icon")]
    public string Icon { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("hideBreadcrumb")]
    public bool HideBreadcrumb { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("currentActiveMenu")]
    public string CurrentActiveMenu { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("frameSrc")]
    public string FrameSrc { get; set; }
}
