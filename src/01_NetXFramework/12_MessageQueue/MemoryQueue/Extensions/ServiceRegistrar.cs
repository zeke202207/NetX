﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace NetX.MemoryQueue
{
    internal static class ServiceRegistrar
    {
        /// <summary>
        /// 添加消息队列类
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembliesToScan"></param>
        internal static void AddMemoryQueueClasses(IServiceCollection services, IEnumerable<Assembly> assembliesToScan)
        {
            assembliesToScan = (assembliesToScan as Assembly[] ?? assembliesToScan).Distinct().ToArray();
            ConnectImplementationsToTypesClosing(typeof(IConsumer<>), services, assembliesToScan, true);
        }

        /// <summary>
        /// 添加需要的处理程序集
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceConfiguration"></param>
        internal static void AddRequiredServices(IServiceCollection services, MemoryQueueServiceConfiguration serviceConfiguration)
        {
            services.AddTransient<ServiceFactory>(p => p.GetService);
            services.Add(new ServiceDescriptor(typeof(IPublisher), serviceConfiguration.QueueImplementationType, serviceConfiguration.Lifetime));
        }

        /// <summary>
        /// 用于区分请求处理程序和通知处理程序之间的行为。  
        /// 请求处理程序应该只添加一次(因此将addIfAlreadyExists设置为false)  
        /// 添加所有的通知处理程序(设置addIfAlreadyExists为true)  
        /// </summary>
        /// <param name="openRequestInterface"></param>
        /// <param name="services"></param>
        /// <param name="assembliesToScan"></param>
        /// <param name="addIfAlreadyExists"></param>
        private static void ConnectImplementationsToTypesClosing(Type openRequestInterface,
            IServiceCollection services,
            IEnumerable<Assembly> assembliesToScan,
            bool addIfAlreadyExists)
        {
            var concretions = new List<Type>();
            var interfaces = new List<Type>();
            foreach (var type in assembliesToScan.SelectMany(a => a.DefinedTypes).Where(t => !t.IsOpenGeneric()))
            {
                var interfaceTypes = type.FindInterfacesThatClose(openRequestInterface).ToArray();
                if (!interfaceTypes.Any())
                    continue;
                if (type.IsConcrete())
                    concretions.Add(type);
                foreach (var interfaceType in interfaceTypes)
                {
                    interfaces.Fill(interfaceType);
                }
            }

            foreach (var @interface in interfaces)
            {
                var exactMatches = concretions.Where(x => x.CanBeCastTo(@interface)).ToList();
                if (addIfAlreadyExists)
                {
                    foreach (var type in exactMatches)
                    {
                        services.AddTransient(@interface, type);
                    }
                }
                else
                {
                    if (exactMatches.Count > 1)
                        exactMatches.RemoveAll(m => !IsMatchingWithInterface(m, @interface));
                    foreach (var type in exactMatches)
                    {
                        services.TryAddTransient(@interface, type);
                    }
                }

                if (!@interface.IsOpenGeneric())
                    AddConcretionsThatCouldBeClosed(@interface, concretions, services);
            }
        }

        private static bool IsMatchingWithInterface(Type handlerType, Type handlerInterface)
        {
            if (handlerType == null || handlerInterface == null)
                return false;
            if (handlerType.IsInterface)
            {
                if (handlerType.GenericTypeArguments.SequenceEqual(handlerInterface.GenericTypeArguments))
                    return true;
            }
            else
                return IsMatchingWithInterface(handlerType.GetInterface(handlerInterface.Name), handlerInterface);
            return false;
        }

        private static void AddConcretionsThatCouldBeClosed(Type @interface, List<Type> concretions, IServiceCollection services)
        {
            foreach (var type in concretions
                .Where(x => x.IsOpenGeneric() && x.CouldCloseTo(@interface)))
            {
                try
                {
                    services.TryAddTransient(@interface, type.MakeGenericType(@interface.GenericTypeArguments));
                }
                catch (Exception)
                {
                }
            }
        }

        private static bool CouldCloseTo(this Type openConcretion, Type closedInterface)
        {
            var openInterface = closedInterface.GetGenericTypeDefinition();
            var arguments = closedInterface.GenericTypeArguments;
            var concreteArguments = openConcretion.GenericTypeArguments;
            return arguments.Length == concreteArguments.Length && openConcretion.CanBeCastTo(openInterface);
        }

        private static bool CanBeCastTo(this Type pluggedType, Type pluginType)
        {
            if (pluggedType == null)
                return false;
            if (pluggedType == pluginType)
                return true;
            return pluginType.GetTypeInfo().IsAssignableFrom(pluggedType.GetTypeInfo());
        }

        private static IEnumerable<Type> FindInterfacesThatClosesCore(Type pluggedType, Type templateType)
        {
            if (pluggedType == null)
                yield break;
            if (!pluggedType.IsConcrete())
                yield break;
            if (templateType.GetTypeInfo().IsInterface)
            {
                var pluggedTypes = (templateType.GetTypeInfo().IsGenericType)
                    ? pluggedType.GetInterfaces().Where(type => type.GetTypeInfo().IsGenericType && (type.GetGenericTypeDefinition() == templateType))
                    : pluggedType.GetInterfaces().Where(type => type == templateType);
                foreach (var interfaceType in pluggedTypes)
                {
                    yield return interfaceType;
                }
            }
            else if (pluggedType.GetTypeInfo().BaseType.GetTypeInfo().IsGenericType &&
                     (pluggedType.GetTypeInfo().BaseType.GetGenericTypeDefinition() == templateType))
            {
                yield return pluggedType.GetTypeInfo().BaseType;
            }

            if (pluggedType.GetTypeInfo().BaseType == typeof(object))
                yield break;
            foreach (var interfaceType in FindInterfacesThatClosesCore(pluggedType.GetTypeInfo().BaseType, templateType))
            {
                yield return interfaceType;
            }
        }

        private static bool IsConcrete(this Type type)
        {
            return !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface;
        }

        private static void Fill<T>(this IList<T> list, T value)
        {
            if (list.Contains(value))
                return;
            list.Add(value);
        }

        private static bool IsOpenGeneric(this Type type)
        {
            return type.GetTypeInfo().IsGenericTypeDefinition || type.GetTypeInfo().ContainsGenericParameters;
        }

        private static IEnumerable<Type> FindInterfacesThatClose(this Type pluggedType, Type templateType)
        {
            return FindInterfacesThatClosesCore(pluggedType, templateType).Distinct();
        }

    }
}
