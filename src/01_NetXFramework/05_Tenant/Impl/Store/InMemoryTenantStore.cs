using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetX.Common;

namespace NetX.Tenants;

/// <summary>
/// 内存租户信息存储
/// </summary>
public class InMemoryTenantStore : ITenantStore<Tenant>
{
    private readonly TenantOption _tenantOption;

    /// <summary>
    /// 内存租户信息存储实例
    /// </summary>
    /// <param name="tenantOption"></param>
    public InMemoryTenantStore(TenantOption tenantOption)
    {
        _tenantOption = tenantOption;
    }

    /// <summary>
    /// 根据租户身份获取租户信息
    /// </summary>
    /// <param name="Identifier">租户身份</param>
    /// <returns></returns>
    public async Task<Tenant?> GetTenantAsync(string Identifier)
    {
        if (_tenantOption.TenantType == TenantType.Multi)
        {
            var tenant = InMemoryTenantProvider.Instance.Tenants
            .SingleOrDefault(p => p.Identifier.Trim().ToLower().Equals(Identifier.Substring(0, Identifier.IndexOf(".")).Trim().ToLower()));
            return await Task.FromResult(tenant);
        }
        else
        {
            var tenant = InMemoryTenantProvider.Instance.Tenants.FirstOrDefault();
            return await Task.FromResult(tenant);
        }
    }
}

internal class InMemoryTenantProvider
{
    private static Lazy<InMemoryTenantProvider> _instance = new Lazy<InMemoryTenantProvider>(() => new InMemoryTenantProvider());
    public List<Tenant> Tenants = new List<Tenant>();

    internal InMemoryTenantProvider()
    {
        Init();
    }

    public static InMemoryTenantProvider Instance { get { return _instance.Value; } }

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
        var tenants = config.GetSection("tenants").Get<Tenant[]>();
        if (tenants != null && tenants.Length > 0)
            Tenants.AddRange(tenants);
    }
}
