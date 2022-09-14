using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NetX.Authentication.Core;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetX.Authentication.Jwt;

/// <summary>
/// Jwt 登录处理
/// </summary>
public class JwtLoginHandler : ILoginHandler
{
    private readonly ILogger _logger;
    private readonly JwtOptions _options;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <param name="logger"></param>
    public JwtLoginHandler(JwtOptions options, ILogger<JwtLoginHandler> logger)
    {
        _options = options;
        _logger = logger;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="claimModel"></param>
    /// <param name="extendData"></param>
    /// <returns></returns>
    public dynamic Handle(ClaimModel claimModel, string extendData)
    {
        var token = Build(claimModel.ToClaims());
        _logger.LogDebug("生成JwtToken：{token}", token);
        return new JwtTokenModel
        {
            AccessToken = token,
            ExpiresIn = _options.Expires * 60,
            /*
             * 刷新token，生成唯一标识，存放在数据库或者redis缓存中，当请求舒心缓存的时候，获取用户信息，重新生成用户访问token。
             * 一般情况下访问token有效期比较短（2h），刷新token比较长（7D）
            */
            RefreshToken = extendData
        };
    }

    /// <summary>
    /// 生成token信息
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    private string Build(Claim[] claims)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(_options.Issuer, _options.Audience, claims, DateTime.Now, DateTime.Now.AddMinutes(_options.Expires), signingCredentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// 解析Token
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    private Claim[] ResolveJWT(string token)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        return new JwtSecurityTokenHandler().ReadJwtToken(token).Claims.ToArray();
    }
}
