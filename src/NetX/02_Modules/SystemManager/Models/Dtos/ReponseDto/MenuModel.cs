using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class MenuModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("parentId")]
    public string ParentId { get; set; }
    [JsonPropertyName("path")]
    public string Path { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("component")]
    public string Component { get; set; }
    [JsonPropertyName("redirect")]
    public string Redirect { get; set; }
    [JsonPropertyName("meta")]
    public MenuMetaData Meta { get; set; }
    [JsonPropertyName("icon")]
    public string Icon { get; set; }
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("permission")]
    public string Permission { get; set; }
    [JsonPropertyName("orderNo")]
    public int OrderNo { get; set; }
    [JsonPropertyName("createTime")]
    public DateTime CreateTime { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("children")]
    public List<MenuModel> Children { get; set; }
}

public enum MenuType :int
{
    Directory =0,
    Menu,
    Button
}
