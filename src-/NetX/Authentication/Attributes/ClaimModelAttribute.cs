using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Authentication;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ClaimModelAttribute : Attribute
{
    public string ClaimKey { get; private set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="claimKey"></param>
    public ClaimModelAttribute(string claimKey)
    {
        ClaimKey = claimKey;
    }
}
