using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class MenuMetaData
{
    [JsonPropertyName("title")]
    public string Title { get; set; }
    [JsonPropertyName("hideMenu")]
    public bool HideMenu { get; set; }
    [JsonPropertyName("ignoreKeepAlive")]
    public bool IgnoreKeepAlive { get; set; }
    [JsonPropertyName("hideChildrenInMenu")]
    public bool HideChildrenMenu { get; set; }
    [JsonPropertyName("icon")]
    public string Icon { get; set; }
    [JsonPropertyName("hideBreadcrumb")]
    public bool HideBreadcrumb { get; set; }
    [JsonPropertyName("currentActiveMenu")]
    public string CurrentActiveMenu { get; set; }
}
