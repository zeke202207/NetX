using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tenants.Extensions
{
    public static class ServiceScopeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="tenantId"></param>
        /// <param name="scopeAction"></param>
        /// <exception cref="KeyNotFoundException"></exception>
        public static void MockTenantScope(this IServiceProvider serviceProvider, string tenantId ,Action<IServiceScope, Tenant> scopeAction)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var tenantStore = scope.ServiceProvider.GetService<ITenantStore<Tenant>>();
                var allTenant = tenantStore.GetAllTenantAsync().GetAwaiter().GetResult();
                var jobTenant = allTenant?.FirstOrDefault(p => p.TenantId.Equals(tenantId));
                if (null == jobTenant)
                    throw new KeyNotFoundException("没有找到租户信息");
                //init the current tenant information
                var tenantOption = scope.ServiceProvider.GetRequiredService<TenantOption>();
                if (null != tenantOption)
                    TenantContext.CurrentTenant.InitPrincipal(new NetXPrincipal(jobTenant), tenantOption);
                scopeAction?.Invoke(scope, jobTenant);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="scopeAction"></param>
        public static void MockTenantScope(this IServiceProvider serviceProvider, Action<IServiceScope, Tenant> scopeAction)
        {
            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                var tenantStore = scope.ServiceProvider.GetService<ITenantStore<Tenant>>();
                foreach (var tenant in tenantStore.GetAllTenantAsync().GetAwaiter().GetResult())
                {
                    var tenantOption = scope.ServiceProvider.GetRequiredService<TenantOption>();
                    if (null != tenantOption)
                        TenantContext.CurrentTenant.InitPrincipal(new NetXPrincipal(tenant), tenantOption);
                    scopeAction?.Invoke(scope, tenant);
                }
            }
        }
    }
}
