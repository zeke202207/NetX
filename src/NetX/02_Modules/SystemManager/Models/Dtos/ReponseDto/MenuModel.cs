using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class MenuModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("parentid")]
    public string ParentId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("component")]
    public string Component { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("redirect")]
    public string Redirect { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("meta")]
    public MenuMetaData Meta { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("icon")]
    public string Icon { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("permission")]
    public string Permission { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("orderno")]
    public int OrderNo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("createtime")]
    public DateTime CreateTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("children")]
    public List<MenuModel> Children { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("show")]
    public string Show { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("isext")]
    public string IsExt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("extpath")]
    public string ExtPath { get; set; }
}
