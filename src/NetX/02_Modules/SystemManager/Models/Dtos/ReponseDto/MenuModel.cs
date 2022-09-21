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
    [JsonPropertyName("parentid")]
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
    [JsonPropertyName("orderno")]
    public int OrderNo { get; set; }
    [JsonPropertyName("createtime")]
    public DateTime CreateTime { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("children")]
    public List<MenuModel> Children { get; set; }
    [JsonPropertyName("show")]
    public string Show { get; set; }
    [JsonPropertyName("isext")]
    public string IsExt { get; set; }
    [JsonPropertyName("extpath")]
    public string ExtPath { get; set; }
}
