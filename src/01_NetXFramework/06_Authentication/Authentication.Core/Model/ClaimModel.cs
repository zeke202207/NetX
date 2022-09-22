using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Authentication.Core;

/// <summary>
/// 
/// </summary>
public class ClaimModel
{
    /// <summary>
    /// 
    /// </summary>
    [ClaimModelAttribute("userid")]
    public string? UserId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [ClaimModelAttribute("loginname")]
    public string? LoginName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    [ClaimModelAttribute("displayname")]
    public string? DisplayName { get; set; }
}
