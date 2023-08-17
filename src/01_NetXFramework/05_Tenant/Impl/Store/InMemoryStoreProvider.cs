using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetX.Common;

namespace NetX.Tenants;

internal class InMemoryStoreProvider
{
    private static Lazy<InMemoryStoreProvider> _instance = new Lazy<InMemoryStoreProvider>(() => new InMemoryStoreProvider());
    public List<Tenant> Tenants = new List<Tenant>();

    internal InMemoryStoreProvider()
    {
        Init();
    }

    public static InMemoryStoreProvider Instance { get { return _instance.Value; } }

    /// <summary>
    /// 配置文件初始化Tenants列表
    /// </summary>
    private void Init()
    {
        if (null == ServiceLocator.Instance)
            return;
        var config = ServiceLocator.Instance.GetService<IConfiguration>();
        if (null == config)
            return;
        var tenants = config.GetSection(TenantConst.C_TENANT_CONFIG_TENANTS).Get<Tenant[]>();
        if (tenants != null && tenants.Length > 0)
            Tenants.AddRange(tenants);
    }
}
