﻿using Newtonsoft.Json;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class MenuRequestModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("id")]
    public string? Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("parentid")]
    public string? ParentId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("path")]
    public string? Path { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("title")]
    public string? Title { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("component")]
    public string? Component { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("redirect")]
    public string? Redirect { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("meta")]
    public MenuMetaData? Meta { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("icon")]
    public string? Icon { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("type")]
    public string Type { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("permission")]
    public string? Permission { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("orderno")]
    public int? OrderNo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("createtime")]
    public DateTime? CreateTime { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("status")]
    public string? Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("isext")]
    public int IsExt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("keepalive")]
    public int KeepAlive { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("show")]
    public int Show { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("extpath")]
    public string? ExtPath { get; set; }
}
