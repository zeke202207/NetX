using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class MenuMetaData
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("hideMenu")]
    public bool HideMenu { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("ignoreKeepAlive")]
    public bool IgnoreKeepAlive { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("hideChildrenInMenu")]
    public bool HideChildrenMenu { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("icon")]
    public string Icon { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("hideBreadcrumb")]
    public bool HideBreadcrumb { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("currentActiveMenu")]
    public string CurrentActiveMenu { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("frameSrc")]
    public string FrameSrc { get; set; }
}
