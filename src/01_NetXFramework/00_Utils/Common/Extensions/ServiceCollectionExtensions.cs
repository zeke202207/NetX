using Microsoft.Extensions.DependencyInjection;
using NetX.Common.Attributes;
using System.Reflection;

namespace NetX.Common;

/// <summary>
/// 服务注入扩展方法
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 程序集注入服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assemblies"></param>
    /// <returns></returns>
    public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services, Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            services.AddServicesFromAssembly(assembly);
        }
        return services;
    }

    /// <summary>
    /// 从指定程序集中注入服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        if (services.IsReadOnly)
            return services;
        foreach (var type in assembly.GetTypes())
        {
            #region ==单例注入==

            var singleton = Attribute.GetCustomAttribute(type, typeof(SingletonAttribute));
            if (singleton is SingletonAttribute)
            {
                var singletonAttr = (SingletonAttribute)singleton;
                //注入自身类型
                if (singletonAttr.Itself)
                {
                    services.AddSingleton(type);
                    continue;
                }
                var interfaces = type.GetInterfaces().Where(m => m != typeof(IDisposable)).ToList();
                if (interfaces.Any())
                {
                    foreach (var i in interfaces)
                    {
                        services.AddSingleton(i, type);
                    }
                }
                else
                {
                    services.AddSingleton(type);
                }
                continue;
            }

            #endregion

            #region ==瞬时注入==

            var transient = Attribute.GetCustomAttribute(type, typeof(TransientAttribute));
            if (transient is TransientAttribute)
            {
                var transientAttr = (TransientAttribute)transient;
                //注入自身类型
                if (transientAttr.Itself)
                {
                    services.AddSingleton(type);
                    continue;
                }
                var interfaces = type.GetInterfaces().Where(m => m != typeof(IDisposable)).ToList();
                if (interfaces.Any())
                {
                    foreach (var i in interfaces)
                    {
                        services.AddTransient(i, type);
                    }
                }
                else
                {
                    services.AddTransient(type);
                }
                continue;
            }

            #endregion

            #region ==Scoped注入==

            var scoped = Attribute.GetCustomAttribute(type, typeof(ScopedAttribute));

            if (scoped is ScopedAttribute)
            {
                var scopedAttr = (ScopedAttribute)scoped;
                //注入自身类型
                if (scopedAttr.Itself)
                {
                    services.AddSingleton(type);
                    continue;
                }
                var interfaces = type.GetInterfaces().Where(m => m != typeof(IDisposable)).ToList();
                if (interfaces.Any())
                {
                    foreach (var i in interfaces)
                    {
                        services.AddScoped(i, type);
                    }
                }
                else
                {
                    services.AddScoped(type);
                }
            }

            #endregion
        }

        return services;
    }
}
