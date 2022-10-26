using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Authentication.Core;

/// <summary>
/// Token声明
/// </summary>
public class ClaimModel
{
    /// <summary>
    /// 用户唯一标识
    /// </summary>
    [ClaimModelAttribute("userid")]
    public string? UserId { get; set; }

    /// <summary>
    /// 登录名
    /// </summary>
    [ClaimModelAttribute("loginname")]
    public string? LoginName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [ClaimModelAttribute("displayname")]
    public string? DisplayName { get; set; }

    /// <summary>
    /// 角色唯一标识
    /// </summary>
    [ClaimModelAttribute("roleid")]
    public string? RoleId { get; set; }
}
