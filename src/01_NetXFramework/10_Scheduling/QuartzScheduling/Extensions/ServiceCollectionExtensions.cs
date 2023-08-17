using Microsoft.Extensions.DependencyInjection;
using NetX.Common;
using System.Reflection;

namespace NetX.QuartzScheduling;

/// <summary>
/// 服务注册扩展类
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加<see cref="NetX.QuartzScheduling"/>服务
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
    /// 添加<see cref="NetX.QuartzScheduling"/>服务
    /// </summary  
    /// <param name="service"></param>           
    /// <param name="injectJobs"></param>
    /// <returns></returns>
    public static IServiceCollection AddQuartzScheduling(this IServiceCollection service, Dictionary<Guid, IEnumerable<Type>> injectJobs, Dictionary<Guid, bool> userModuleOptions)
    {
        AddQuartzScheduling(service);
        if (!injectJobs.Any())
            return service;
        foreach (var injectJob in injectJobs)
        {
            injectJob.Value.ToList().ForEach(job =>
            {
                var jobAttribute = job.GetCustomAttribute<JobTaskAttribute>();
                if (null == jobAttribute)
                    return;
                JobTaskTypeManager.Instance.Add(jobAttribute.Id, new JobTaskTypeModel()
                {
                    Id = jobAttribute.Id,
                    DisplayName = jobAttribute.Name,
                    JobTaskType = job,
                    Enabled = userModuleOptions.FirstOrDefault(a => a.Key.Equals(injectJob.Key)).Value
                });
                if (!typeof(IJobTask).IsAssignableFrom(job))
                    return;
                service.AddTransient(job);
            });
        }

        //injectJobs.ToList().ForEach(job =>
        //{
        //    var jobAttribute = job.GetCustomAttribute<JobTaskAttribute>();
        //    if (null == jobAttribute)
        //        return;
        //    JobTaskTypeManager.Instance.Add(jobAttribute.Id, new JobTaskTypeModel()
        //    {
        //        Id = jobAttribute.Id,
        //        DisplayName = jobAttribute.Name,
        //        JobTaskType = job,
        //    });
        //    if (!typeof(IJobTask).IsAssignableFrom(job))
        //        return;
        //    service.AddTransient(job);
        //});
        return service;
    }
}
