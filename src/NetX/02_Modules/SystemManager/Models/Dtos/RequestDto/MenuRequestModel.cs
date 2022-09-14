﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class MenuRequestModel
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("parentid")]
    public string? ParentId { get; set; }
    [JsonPropertyName("path")]
    public string? Path { get; set; }
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("component")]
    public string? Component { get; set; }
    [JsonPropertyName("redirect")]
    public string? Redirect { get; set; }
    [JsonPropertyName("meta")]
    public MenuMetaData? Meta { get; set; }
    [JsonPropertyName("icon")]
    public string? Icon { get; set; }
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    [JsonPropertyName("permission")]
    public string? Permission { get; set; }
    [JsonPropertyName("orderNo")]
    public int? OrderNo { get; set; }
    [JsonPropertyName("createTime")]
    public DateTime? CreateTime { get; set; }
    [JsonPropertyName("status")]
    public string? Status { get; set; }
    [JsonPropertyName("isExt")]
    public int IsExt { get; set; }
    [JsonPropertyName("keepAlive")]
    public int KeepAlive { get; set; }
    [JsonPropertyName("show")]
    public int Show { get; set; }
}