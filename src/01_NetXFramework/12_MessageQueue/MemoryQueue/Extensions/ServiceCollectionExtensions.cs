using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace NetX.MemoryQueue
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 从指定程序集注册处理程序和队列类型
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMemoryQueue(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services.AddMemoryQueue(assemblies, configuration: null);
        }

        /// <summary>
        /// 从指定程序集注册处理程序和队列类型
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        public static IServiceCollection AddMemoryQueue(this IServiceCollection services, Action<MemoryQueueServiceConfiguration> configuration, params Assembly[] assemblies)
        {
            return services.AddMemoryQueue(assemblies, configuration);
        }

        /// <summary>
        /// 从指定程序集注册处理程序和队列类型
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static IServiceCollection AddMemoryQueue(this IServiceCollection services, IEnumerable<Assembly> assemblies, Action<MemoryQueueServiceConfiguration> configuration)
        {
            if (!assemblies.Any())
                throw new ArgumentException("没有发现要扫描的程序集。 提供至少一个组件来扫描处理程序。");
            var serviceConfig = new MemoryQueueServiceConfiguration();
            configuration?.Invoke(serviceConfig);
            ServiceRegistrar.AddRequiredServices(services, serviceConfig);
            ServiceRegistrar.AddMemoryQueueClasses(services, assemblies);
            return services;
        }

        /// <summary>
        /// 从指定程序集注册处理程序和队列类型
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblyMarkerTypes"></param>        
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMemoryQueue(this IServiceCollection services, params Type[] handlerAssemblyMarkerTypes)
            => services.AddMemoryQueue(handlerAssemblyMarkerTypes, configuration: null);

        /// <summary>
        /// 从指定程序集注册处理程序和队列类型
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblyMarkerTypes"></param>
        /// <param name="configuration">用于配置选项的操作</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMemoryQueue(this IServiceCollection services, Action<MemoryQueueServiceConfiguration> configuration, params Type[] handlerAssemblyMarkerTypes)
            => services.AddMemoryQueue(handlerAssemblyMarkerTypes, configuration);

        /// <summary>
        /// 从指定程序集注册处理程序和队列类型
        /// </summary>
        /// <param name="services"></param>
        /// <param name="handlerAssemblyMarkerTypes"></param>
        /// <param name="configuration">用于配置选项的操作</param>
        /// <returns>Service collection</returns>
        public static IServiceCollection AddMemoryQueue(this IServiceCollection services, IEnumerable<Type> handlerAssemblyMarkerTypes, Action<MemoryQueueServiceConfiguration> configuration)
            => services.AddMemoryQueue(handlerAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly), configuration);
    }
}
