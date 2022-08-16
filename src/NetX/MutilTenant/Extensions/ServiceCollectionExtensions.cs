using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.MutilTenant;

/// <summary>
/// 多租户注入
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 多租户注入
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="service"></param>
    /// <returns></returns>
    public static TenantBuilder<T> AddMultiTenancy<T>(this IServiceCollection service)
        where T : Tenant
    => new TenantBuilder<T>(service);

    /// <summary>
    /// 多租户注入 
    /// </summary>
    /// <param name="service"></param>
    /// <returns></returns>
    public static TenantBuilder<Tenant> AddMutiTenancy(this IServiceCollection service)
    => new TenantBuilder<Tenant>(service);

}
