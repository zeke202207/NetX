namespace NetX.Authentication.JWT;

/// <summary>
/// JWT令牌
/// </summary>
public class JwtTokenModel
{
    /// <summary>
    /// jwt令牌
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public string RefreshToken { get; set; }

    /// <summary>
    /// 有效期(秒)
    /// 对接口调用方而言，非严格意义上的过期，从调用接口前开始计算时间
    /// </summary>
    public int ExpiresIn { get; set; }
}
