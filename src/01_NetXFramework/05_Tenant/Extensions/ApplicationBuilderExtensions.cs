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

    /// <summary>
    /// 使用租户数据库
    /// </summary>
    /// <param name="app"></param>
    /// <returns></returns>
    public static IApplicationBuilder UserTenancyDatabase(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            using var scope = app.ApplicationServices.CreateScope();
            var _freeSql = scope.ServiceProvider.GetService<IFreeSql>() as FreeSqlCloud<string>;
            try
            {
                //处理Tenant信息
                var accessor = scope.ServiceProvider.GetService<ITenantAccessor<Tenant>>();
                var tenantOptions = scope.ServiceProvider.GetService<TenantOption>();
                if (null != accessor && null != tenantOptions)
                    TenantContext.CurrentTenant.InitPrincipal(new NetXPrincipal(accessor.Tenant), tenantOptions);
                if (null != TenantContext.CurrentTenant.Principal)
                {
                    _freeSql?.Register(TenantContext.CurrentTenant.Principal.Tenant.TenantId, () =>
                    {
                        if (null == TenantContext.CurrentTenant.DatabaseInfo)
                            return null;
                        var db = new FreeSqlBuilder().UseConnectionString(
                            TenantContext.CurrentTenant.DatabaseInfo.DatabaseType.ToDatabaseType(),
                            TenantContext.CurrentTenant.ConnectionStr)
                        .Build();
                    //db.Aop.CommandAfter += ...
                        return db;
                    });
                    // 切换租户
                    _freeSql?.Change(TenantContext.CurrentTenant.Principal.Tenant.TenantId);
                }
                await next();
            }
            finally
            {
                // 切换回 main 库
                _freeSql?.Change("main");
            }
        });
        return app;
    }
}
