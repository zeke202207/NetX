using System.Text.Json.Serialization;

namespace NetX.SystemManager.Models;

/// <summary>
/// 
/// </summary>
public class RoleRequestModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("rolename")]
    public string RoleName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("remark")]
    public string? Remark { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("menu")]
    public CheckMenu? Menus { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<string> ToMenuList()
    {
        List<string> result = new List<string>();
        if (null == Menus)
            return result;
        if (null != Menus.Checked)
            result.AddRange(Menus.Checked);
        if (null != Menus.HalfChecked)
            result.AddRange(Menus.HalfChecked);
        return result;
    }
}

/// <summary>
/// 
/// </summary>
public class RoleStatusModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("id")]
    public string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonPropertyName("status")]
    public string Status { get; set; }
}

/// <summary>
/// 
/// </summary>
public class CheckMenu
{
    /// <summary>
    /// 
    /// </summary>
    public List<string> Checked { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<string> HalfChecked { get; set; }
}
