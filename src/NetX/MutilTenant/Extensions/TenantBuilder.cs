using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MutilTenant;

/// <summary>
/// 配置tenant的services 
/// </summary>
public class TenantBuilder<T>
    where T : Tenant
{
    private readonly IServiceCollection _services;

    /// <summary>
    /// 租户注入构建器
    /// </summary>
    /// <param name="services"></param>
    public TenantBuilder(IServiceCollection services)
    {
        services.AddTransient<TenantAccessService<T>>();
        services.AddTransient<ITenantAccessor<T>, TenantAccessor<T>>();
        _services = services;
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
        where V :class, ITenantStore<T>
    {
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
}
