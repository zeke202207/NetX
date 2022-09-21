using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace NetX.SystemManager.Models;

public class RoleRequestModel
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("rolename")]
    public string RoleName { get; set; }
    [JsonPropertyName("status")]
    public string Status { get; set; }
    [JsonPropertyName("remark")]
    public string? Remark { get; set; }
    [JsonPropertyName("menu")]
    public CheckMenu? Menus { get; set; }

    public List<string> ToMenuList()
    {
        List<string> result = new List<string>();
        if (null == Menus)
            return result;
        if(null!= Menus.Checked)
            result.AddRange(Menus.Checked);
        if(null!= Menus.HalfChecked)
            result.AddRange(Menus.HalfChecked);
        return result;
    }
}

public class RoleStatusModel
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }
    [JsonPropertyName("status")]
    public string? Status { get; set; }
}

public class CheckMenu
{
    public List<string> Checked { get; set; }
    public List<string> HalfChecked { get; set; }
}
