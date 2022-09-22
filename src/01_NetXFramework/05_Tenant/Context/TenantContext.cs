namespace NetX.Tenants;

/// <summary>
/// 租户上下文
/// </summary>
public class TenantContext
{
    private static readonly AsyncLocal<TenantContext> _instance;

    private TenantOption? _tenantOption;
    
    static TenantContext()
    {
        _instance = new AsyncLocal<TenantContext>();
    }

    /// <summary>
    /// 当前线程的租户上下文信息
    /// </summary>
    public static TenantContext CurrentTenant
    {
        get
        {
            if (null == _instance.Value)
                _instance.Value = new TenantContext();
            return _instance.Value;
        }
        private set
        {
            _instance.Value = value;
        }
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
    /// 数据库配置信息 
    /// </summary>
    internal DatabaseInfo? DatabaseInfo
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
    /// 数据库连接字符串
    /// </summary>
    public string? ConnectionStr => DatabaseInfo?.ToConnStr(this.TenantType, this.Principal?.Tenant.TenantId);

    /// <summary>
    /// 创建数据需要的连接字符串
    /// </summary>
    public string? CreateSchemaConnectionStr => DatabaseInfo?.ToCreateDatabaseConnStr();

    /// <summary>
    /// Schema Name
    /// </summary>
    public string? DatabaseName => DatabaseInfo?.ToDatabaseName(this.TenantType, this.Principal?.Tenant.TenantId);


    /// <summary>
    /// 初始化租户信息
    /// </summary>
    /// <param name="principal"></param>
    /// <param name="tenantOption"></param>
    public void InitPrincipal(NetXPrincipal principal, TenantOption tenantOption)
    {
        this.Principal = principal;
        this._tenantOption = tenantOption;
        this.TenantType = tenantOption.TenantType;
        this.DatabaseInfo = tenantOption.DatabaseInfo;
        //身份信息
        this.IsAuthenticated = principal.IsAuthenticated;
    }

    /// <summary>
    /// 主体对象
    /// </summary>
    public NetXPrincipal? Principal { get; private set; }

    /// <summary>
    /// 当前租户类型
    /// </summary>
    public TenantOption? TenantOption { get { return _tenantOption; } }
}
