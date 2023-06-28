using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetX.MemoryQueue;
using System.Reflection;

namespace NetX.AuditLog
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 审计日志注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="auditOption"></param>
        /// <param name="storeProvider"> typeof <see cref="IAuditLogStoreProvider"/>类型</param>
        /// <returns></returns>
        public static IServiceCollection AddAuditLog(this IServiceCollection services, Action<AuditOption> auditOption, Type storeProvider = null)
        {
            services.AddScoped<IClientProvider, HttpContextClientInfoProvider>();
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(AuditLogActionFilter));
            });
            AuditOption option = new AuditOption();
            auditOption?.Invoke(option);
            services.AddSingleton(option);
            //添加消息队列
            services.AddMemoryQueue(p => p.AsScoped(), new Assembly[] { Assembly.GetExecutingAssembly() });
            //添加消息处理者
            if (null == storeProvider)
                services.AddScoped<IAuditLogStoreProvider, DefaultAuditLogStore>();
            else
                services.AddScoped(typeof(IAuditLogStoreProvider), storeProvider);
            return services;
        }
    }
}
