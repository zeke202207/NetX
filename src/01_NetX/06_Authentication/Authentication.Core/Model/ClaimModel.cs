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
    [ClaimModelAttribute("userid")]
    public string UserId { get; set; }

    [ClaimModelAttribute("loginname")]
    public string LoginName { get; set; }

    [ClaimModelAttribute("displayname")]
    public string DisplayName { get; set; }
}
