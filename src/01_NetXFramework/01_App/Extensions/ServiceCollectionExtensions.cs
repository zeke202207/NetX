using Microsoft.Extensions.DependencyInjection;
using NetX.Common.Attributes;
using System.Reflection;

namespace NetX.App;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 从指定程序集中注入服务
    /// </summary>
    /// <param name="services"></param>
    /// <param name="assembly"></param>
    /// <returns></returns>
    public static IServiceCollection AddServicesFromAssembly(this IServiceCollection services, Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            #region ==单例注入==

            var singletonAttr = (SingletonAttribute)Attribute.GetCustomAttribute(type, typeof(SingletonAttribute));
            if (singletonAttr != null)
            {
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

            var transientAttr = (TransientAttribute)Attribute.GetCustomAttribute(type, typeof(TransientAttribute));
            if (transientAttr != null)
            {
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
            var scopedAttr = (ScopedAttribute)Attribute.GetCustomAttribute(type, typeof(ScopedAttribute));
            if (scopedAttr != null)
            {
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
