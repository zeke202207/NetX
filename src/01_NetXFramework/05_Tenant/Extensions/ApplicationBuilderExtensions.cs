using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace NetX.Tenants;

/// <summary>
/// 
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Use the Teanant Middleware to process the request
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseMultiTenancy<T>(this IApplicationBuilder builder) where T : Tenant
        => builder.UseMiddleware<TenantMiddleware<T>>();


    /// <summary>
    /// Use the Teanant Middleware to process the request
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder builder)
        => builder.UseMiddleware<TenantMiddleware<Tenant>>();
}
