using Newtonsoft.Json;

namespace NetX.RBAC.Models;

/// <summary>
/// 
/// </summary>
public class RoleRequestModel
{
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("id")]
    public string? Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("rolename")]
    public string RoleName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("status")]
    public string Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("remark")]
    public string? Remark { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("menu")]
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
    [JsonProperty("id")]
    public string Id { get; set; }
    /// <summary>
    /// 
    /// </summary>
    [JsonProperty("status")]
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
    public List<string> Checked { get; set; } = new List<string>();
    /// <summary>
    /// 
    /// </summary>
    public List<string> HalfChecked { get; set; } = new List<string>();
}
