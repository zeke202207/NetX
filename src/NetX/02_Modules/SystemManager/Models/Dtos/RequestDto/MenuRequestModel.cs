using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class MenuRequestModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("parentid")]
    public string? ParentId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("path")]
    public string? Path { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("component")]
    public string? Component { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("redirect")]
    public string? Redirect { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("meta")]
    public MenuMetaData? Meta { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("permission")]
    public string? Permission { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("orderNo")]
    public int? OrderNo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("createTime")]
    public DateTime? CreateTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("isExt")]
    public int IsExt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("keepAlive")]
    public int KeepAlive { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("show")]
    public int Show { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("extpath")]
    public string? ExtPath { get; set; }
}
