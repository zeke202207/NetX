using NetX.Authentication.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace NetX.Tenants;

/// <summary>
/// 主体对象
/// 用户信息、角色信息等
/// </summary>
public class NetXPrincipal
{
    /// <summary>
    /// 身份信息
    /// </summary>
    [NonSerialized]
    public IIdentity Identity;

    /// <summary>
    /// 租户信息
    /// </summary>
    public Tenant Tenant
    {
        get;
        private set;
    }

    /// <summary>
    /// 内部唯一标识
    /// </summary>
    public string? UserId
    {
        get;
        private set;
    }

    /// <summary>
    /// 登录名
    /// </summary>
    public string? UserName
    {
        get;
        private set;
    }

    /// <summary>
    /// 显示名
    /// </summary>
    public string? DisplayName
    {
        get;
        private set;
    }

    /// <summary>
    /// 是否已授权
    /// </summary>
    internal bool IsAuthenticated
    {
        get;
        private set;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tenant"></param>
    public NetXPrincipal(Tenant tenant)
    {
        //租户信息
        this.Tenant = tenant;
        this.IsAuthenticated = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="identity"></param>
    /// <param name="tenant"></param>
    public NetXPrincipal(IIdentity identity, Tenant tenant)
    {
        //租户信息
        this.Tenant = tenant;
        this.IsAuthenticated = identity.IsAuthenticated;
        //身份信息
        SetIdentityInfo(identity);
    }


    /// <summary>
    /// 设置身份信息
    /// </summary>
    /// <param name="identity"></param>
    internal void SetIdentityInfo(IIdentity identity)
    {
        //身份信息
        this.Identity = identity;
        var claimsIdentity = identity as ClaimsIdentity;
        if (claimsIdentity != null)
        {
            //解析claims
            var model = claimsIdentity.Claims.ToClaimModel();
            if (null != model)
            {
                this.UserId = model.UserId;
                this.UserName = model.LoginName;
                this.DisplayName = model.DisplayName;
            }
        }
    }
}
