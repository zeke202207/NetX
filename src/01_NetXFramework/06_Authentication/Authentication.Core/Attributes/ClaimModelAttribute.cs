namespace NetX.Authentication.Core;

/// <summary>
/// 
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class ClaimModelAttribute : Attribute
{
    /// <summary>
    /// 
    /// </summary>
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
