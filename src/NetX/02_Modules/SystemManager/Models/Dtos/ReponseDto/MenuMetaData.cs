using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

//public class CurrentUserMenus
//{
//    [JsonPropertyName("id")]
//    public string Id { get; set; }
//    [JsonPropertyName("parentid")]
//    public string ParentId { get; set; }
//    [JsonPropertyName("path")]
//    public string Path { get; set; }
//    [JsonPropertyName("name")]
//    public string Name { get; set; }
//    [JsonPropertyName("component")]
//    public string Component { get; set; }
//    [JsonPropertyName("redirect")]
//    public string Redirect { get; set; }
//    [JsonPropertyName("meta")]
//    public MenuMetaData Meta { get; set; }
//    [JsonPropertyName("children")]
//    public List<CurrentUserMenus> Children { get; set; } = new List<CurrentUserMenus>();
//}

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
