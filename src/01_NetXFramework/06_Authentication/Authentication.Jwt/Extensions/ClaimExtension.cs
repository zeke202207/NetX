﻿using NetX.Authentication.Core;
using System.Reflection;
using System.Security.Claims;

namespace NetX.Authentication.Jwt;

/// <summary>
/// Claim扩展类
/// </summary>
public static class ClaimExtension
{
    private static BindingFlags _flags = BindingFlags.Public | BindingFlags.Instance;

    /// <summary>
    /// 将 <see cref="ClaimModel"/> 转换成 <see cref="Claim"/> 集合
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public static Claim[] ToClaims(this ClaimModel model)
    {
        if (null == model)
            return new Claim[0];
        return model.GetType()
            .GetProperties(_flags).ToList()
            .Where(p => p.GetCustomAttributes(typeof(ClaimModelAttribute), false).Length > 0)
            .Select(p =>
                new Claim(
                    p.GetCustomAttributes<ClaimModelAttribute>(false).FirstOrDefault()?.ClaimKey ?? string.Empty,
                    p.GetValue(model)?.ToString() ?? string.Empty)
                )
            .ToArray();
    }

    /// <summary>
    /// 将 <see cref="Claim"/>集合 转换成 <see cref="ClaimModel"/> 
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    public static ClaimModel? ToClaimModel(this IEnumerable<Claim> claims)
    {
        if (null == claims || claims.Count() == 0)
            return null;
        ClaimModel model = new ClaimModel();
        var properties = typeof(ClaimModel)
            .GetProperties(_flags)
            .Where(p => p.GetCustomAttributes(typeof(ClaimModelAttribute), false).Length > 0);
        foreach (var p in properties)
        {
            var cusAttribute = p.GetCustomAttributes<ClaimModelAttribute>(false).FirstOrDefault();
            if (null == cusAttribute)
                continue;
            string? value = claims.FirstOrDefault(p => p.Type.ToLower().Equals(cusAttribute.ClaimKey.ToLower()))?.Value;
            if (null != value)
                p.SetValue(model, value);
        }
        return model;
    }
}
