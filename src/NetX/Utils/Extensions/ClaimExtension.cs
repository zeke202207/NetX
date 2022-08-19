using NetX.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetX;

internal static class ClaimExtension
{
    private static BindingFlags _flags = BindingFlags.Public | BindingFlags.Instance;

    public static Claim[] ToClaims(this ClaimModel model)
    {
        if(null == model)
            return new Claim[0];
        return model.GetType()
            .GetProperties(_flags).ToList()
            .Where(p => p.GetCustomAttributes(typeof(ClaimModelAttribute), false).Length > 0)
            .Select(p =>
                new Claim(
                    ((ClaimModelAttribute)p.GetCustomAttributes(typeof(ClaimModelAttribute), false).FirstOrDefault()).ClaimKey,
                    p.GetValue(model).ToString())
                )
            .ToArray();
    }

    public static ClaimModel ToClaimModel(this IEnumerable<Claim> claims)
    {
        ClaimModel model = new ClaimModel();
        if (claims?.Count() == 0)
            return model;
        var properties = typeof(ClaimModel)
            .GetProperties(_flags).ToList()
            .Where(p => p.GetCustomAttributes(typeof(ClaimModelAttribute), false).Length > 0);
        foreach(var p in properties)
        {
            var cusAttribute = (ClaimModelAttribute)p.GetCustomAttributes(typeof(ClaimModelAttribute), false).FirstOrDefault();
            if (null == cusAttribute)
                continue;
            string value = claims.FirstOrDefault(p => p.Type.ToLower().Equals(cusAttribute.ClaimKey.ToLower()))?.Value;
            if (null != value)
                p.SetValue(model, value);
        }
        return model;
    }
}
