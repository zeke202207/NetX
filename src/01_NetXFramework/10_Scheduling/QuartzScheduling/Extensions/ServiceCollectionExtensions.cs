using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Netx.QuartzScheduling;

/// <summary>
/// 服务注册扩展类
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加<see cref="Netx.QuartzScheduling"/>服务
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <returns></returns>
    public static IServiceCollection AddQuartzScheduling(this IServiceCollection services)
    {
        services.AddServicesFromAssembly(
            new Assembly[] {
                typeof(QuartzShutdownHandler).Assembly
            });
        //已通过 attribute 统一注入 
        //注入日志
        //注入Quartz服务实例
        //注入应用关闭事件
        return services;
    }

    /// <summary>
    /// 添加<see cref="Netx.QuartzScheduling"/>服务
    /// </summary>
    /// <param name="service"></param>
    /// <param name="injectJobs"></param>
    /// <returns></returns>
    public static IServiceCollection AddQuartzScheduling(this IServiceCollection service, IEnumerable<Type> injectJobs)
    {
        AddQuartzScheduling(service);
        if (!injectJobs.Any())
            return service;
        injectJobs.ToList().ForEach(job =>
        {
            if (!typeof(IJobTask).IsAssignableFrom(job))
                return;
            service.AddTransient(job);
        });
        return service;
    }
}
