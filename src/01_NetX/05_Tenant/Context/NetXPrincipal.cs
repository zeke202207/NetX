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
    /// 租户系统类型
    /// </summary>
    public TenantType TenantType
    {
        get;
        private set;
    }

    /// <summary>
    /// 内部唯一标识
    /// </summary>
    public string UserId
    {
        get;
        private set;
    }

    /// <summary>
    /// 登录名
    /// </summary>
    public string UserName
    {
        get;
        private set;
    }

    /// <summary>
    /// 显示名
    /// </summary>
    public string DisplayName
    {
        get;
        private set;
    }

    /// <summary>
    /// 数据库配置信息 
    /// </summary>
    internal DatabaseInfo DatabaseInfo
    {
        get;
        private set;
    }

    /// <summary>
    /// 数据库连接字符串
    /// </summary>
    public string ConnectionStr => DatabaseInfo.ToConnStr(this.TenantType, this.Tenant?.TenantId);

    /// <summary>
    /// 创建数据需要的连接字符串
    /// </summary>
    public string CreateSchemaConnectionStr => DatabaseInfo.ToCreateDatabaseConnStr();

    /// <summary>
    /// Schema Name
    /// </summary>
    public string DatabaseName => DatabaseInfo.ToDatabaseName(this.TenantType, this.Tenant?.TenantId);

    /// <summary>
    /// 
    /// </summary>
    public NetXPrincipal(IIdentity identity, Tenant tenant, TenantOption tenantOption)
    {
        //租户信息
        this.Tenant = tenant;
        this.TenantType = tenantOption.TenantType;
        this.DatabaseInfo = tenantOption.DatabaseInfo;
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
