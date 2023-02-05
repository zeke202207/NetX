using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using NetX.Authentication.Core;
using System.Text;

namespace NetX.Authentication.Jwt;

/// <summary>
/// 
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加Jwt认证
    /// </summary>
    /// <param name="services"></param>
    /// <param name="config">配置项</param>
    public static IServiceCollection AddJwtAuth(this IServiceCollection services, IConfiguration? config)
    {
        if (null == config)
            return services;
        var jwtOptions = config.GetSection("authentication:jwt").Get<JwtOptions>();
        if (jwtOptions == null)
            return services;
        services.AddSingleton(jwtOptions);
        services.TryAddSingleton<ILoginHandler, JwtLoginHandler>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,//是否验证issuer
                    ValidateAudience = true,//是否验证Audience(接收人)
                    ValidateLifetime = true,//是否验证失效时间
                    ValidateIssuerSigningKey = true,//是否验证signingkey
                    ValidIssuer = jwtOptions.Issuer,//Issuer
                    ValidAudience = jwtOptions.Audience,// Audienc 这两项和前面签发jwt的设置一致
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),//拿到SecurityKey
                    ClockSkew = TimeSpan.Zero
                };
                options.Events = new JwtBearerEvents()
                {
                    OnChallenge = context =>
                    {
                        context.Response.Headers.Add("Token-Error", context.ErrorDescription);
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        // 如果过期，则把<Token过期>添加到，返回头信息中
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            context.Response.Headers.Add("Token-Expired", "true");
                        return Task.CompletedTask;
                    }
                };
            });

        return services;
    }
}
