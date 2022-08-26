using FreeSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace NetX.Tenants;

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
    /// <typeparam name="T"></typeparam>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseMultiTenancy(this IApplicationBuilder builder)
        => builder.UseMiddleware<TenantMiddleware<Tenant>>();

    /// <summary>
    /// 使用租户数据库
    /// </summary>
    /// <param name="app"></param>
    /// <param name="fsql"></param>
    /// <returns></returns>
    public static IApplicationBuilder UserMultiTenancyDatabase(this IApplicationBuilder app)
    {
        app.Use(async (context, next) =>
        {
            var _freeSql = app.ApplicationServices.GetService<IFreeSql>() as FreeSqlCloud<string>;
            try
            {
                //处理Tenant信息
                var accessor = app.ApplicationServices.GetService<ITenantAccessor<Tenant>>();
                var tenantOptions = app.ApplicationServices.GetService<TenantOption>();
                if (null != accessor && null != tenantOptions)
                    TenantContext.Current.Init(new NetXPrincipal(accessor.Tenant, tenantOptions));
                _freeSql?.Register(TenantContext.Current.Principal.Tenant.TenantId, () =>
                {
                    var db = new FreeSqlBuilder().UseConnectionString(
                        TenantContext.Current.Principal.DatabaseInfo.DatabaseType.ToDatabaseType(),
                        TenantContext.Current.Principal.ConnectionStr)
                    .Build();
                    //db.Aop.CommandAfter += ...
                    return db;
                });
                // 切换租户
                _freeSql?.Change(TenantContext.Current.Principal.Tenant.TenantId);
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
