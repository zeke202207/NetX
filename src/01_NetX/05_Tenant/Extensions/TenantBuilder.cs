using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace NetX.Tenants;

/// <summary>
/// 配置tenant的services 
/// </summary>
public class TenantBuilder<T>
    where T : Tenant
{
    private readonly IServiceCollection _services;
    private readonly TenantType _tenantType;
    private readonly TenantOption _option;

    /// <summary>
    /// 租户注入构建器
    /// </summary>
    /// <param name="services"></param>
    /// <param name="tenantType"></param>
    public TenantBuilder(IServiceCollection services, TenantType tenantType = TenantType.Single)
    {
        services.AddTransient<TenantAccessService<T>>();
        services.AddTransient<ITenantAccessor<T>, TenantAccessor<T>>();
        _services = services;
        _tenantType = tenantType;
        _option = new TenantOption(_tenantType);
    }

    /// <summary>
    /// 配置数据库
    /// </summary>
    /// <param name="database"></param>
    /// <returns></returns>
    public TenantBuilder<T> WithDatabaseInfo(DatabaseInfo database)
    {
        _option.DatabaseInfo = new DatabaseInfo()
        {
            DatabaseHost = database.DatabaseHost,
            DatabaseName = database.DatabaseName,
            DatabasePort = database.DatabasePort,
            DatabaseType = database.DatabaseType,
            UserId = database.UserId,
            Password = database.Password,
        };
        _services.AddSingleton<TenantOption>(_option);
        return this;
    }

    /// <summary>
    /// 注册租户解析实现
    /// </summary>
    /// <typeparam name="V"></typeparam>
    /// <param name="lifetime"></param>
    /// <returns></returns>
    public TenantBuilder<T> WithResolutionStrategy<V>(ServiceLifetime lifetime = ServiceLifetime.Transient)
        where V : class, ITenantResolutionStrategy
    {
        if (_tenantType == TenantType.Single)
            return this;
        _services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        _services.Add(ServiceDescriptor.Describe(typeof(ITenantResolutionStrategy), typeof(V), lifetime));
        return this;
    }

    /// <summary>
    /// 注册租户信息存储实现
    /// </summary>
    /// <typeparam name="V"></typeparam>
    /// <param name="serviceLifetime"></param>
    /// <returns></returns>
    public TenantBuilder<T> WithStore<V>(ServiceLifetime serviceLifetime = ServiceLifetime.Transient)
        where V : class, ITenantStore<T>
    {
        if (_tenantType == TenantType.Single)
            return this;
        _services.Add(ServiceDescriptor.Describe(typeof(ITenantStore<T>), typeof(V), serviceLifetime));
        return this;
    }

    /// <summary>
    /// 每个租户注册单独的配置
    /// </summary>
    /// <typeparam name="TOptions">Type of options we are apply configuration to</typeparam>
    /// <param name="tenantConfig">Action to configure options for a tenant</param>
    /// <returns></returns>
    public TenantBuilder<T> WithPerTenantOptions<TOptions>(Action<TOptions, T> tenantConfig) where TOptions : class, new()
    {
        if (_tenantType == TenantType.Single)
            return this;
        //Register the multi-tenant cache
        _services.AddSingleton<IOptionsMonitorCache<TOptions>>(a => ActivatorUtilities.CreateInstance<TenantOptionsCache<TOptions, T>>(a));
        //Register the multi-tenant options factory
        _services.AddTransient<IOptionsFactory<TOptions>>(a => ActivatorUtilities.CreateInstance<TenantOptionsFactory<TOptions, T>>(a, tenantConfig));
        //Register IOptionsSnapshot support
        _services.AddScoped<IOptionsSnapshot<TOptions>>(a => ActivatorUtilities.CreateInstance<TenantOptions<TOptions>>(a));
        //Register IOptions support
        _services.AddSingleton<IOptions<TOptions>>(a => ActivatorUtilities.CreateInstance<TenantOptions<TOptions>>(a));
        return this;
    }

    /// <summary>
    /// 默认单租户注入
    /// </summary>
    /// <returns></returns>
    public TenantBuilder<T> DefaultSingleTenant()
    {
        _services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        _services.Add(ServiceDescriptor.Describe(typeof(ITenantResolutionStrategy), typeof(HostResolutionStrategy), ServiceLifetime.Transient));
        _services.Add(ServiceDescriptor.Describe(typeof(ITenantStore<T>), typeof(InMemoryTenantStore), ServiceLifetime.Transient));
        return this;
    }
}
