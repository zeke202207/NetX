using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetX.SharedFramework.ChainPipeline;
using NetX.SharedFramework.ChainPipeline.ChainDataflow;
using NetX.SharedFramework.ChainPipeline.PipelineDataflow;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NetX.SharedFramework
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 注入中间件
        /// </summary>
        /// <param name="services"></param>
        /// <param name="middlewareCreater"></param>
        /// <param name="middlewares"></param>
        /// <returns></returns>
        internal static IServiceCollection InjectMiddleware(this IServiceCollection services, ServiceLifetime leftTime, Type middlewareCreater, params Type[] middlewares)
        {
            _ = leftTime switch
            {
                ServiceLifetime.Transient => services.AddTransient(typeof(IMiddlewareCreater), middlewareCreater),
                ServiceLifetime.Scoped => services.AddScoped(typeof(IMiddlewareCreater), middlewareCreater),
                ServiceLifetime.Singleton => services.AddSingleton(typeof(IMiddlewareCreater), middlewareCreater),
                _ => throw new NotSupportedException("不支持的注入周期")
            };
            middlewares.ToList().ForEach(p =>
            {
                _ = leftTime switch
                {
                    ServiceLifetime.Scoped => services.AddScoped(p),
                    ServiceLifetime.Transient => services.AddTransient(p),
                    ServiceLifetime.Singleton => services.AddSingleton(p),
                    _ => throw new NotSupportedException("不支持的注入周期")
                };
            });
            return services;
        }

        /// <summary>
        /// 中间件排序
        /// </summary>
        /// <param name="middlewares"></param>
        /// <returns></returns>
        private static IEnumerable<Type> MiddlewaresOrder(params Type[] middlewares)
        {
            foreach (var middleware in middlewares.OrderBy(p => p.GetCustomAttribute<ChainPipelineAttribute>()?.Order))
                yield return middleware;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="services"></param>
        /// <param name="middlewareCreater"></param>
        /// <param name="middlewares"></param>
        /// <returns></returns>
        public static IServiceCollection AddChain<TParameter, TResult>(this IServiceCollection services, ServiceLifetime leftTime, params Type[] middlewares)
                where TResult : class, new()
        {
            //中间件注入
            services.InjectMiddleware(leftTime, typeof(DependencyInjectionCreater), middlewares);
            //管道注入
            _ = leftTime switch
            {
                ServiceLifetime.Scoped => services.AddScoped<IChain<TParameter, TResult>>(s =>
                {
                    var creater = s.GetService<IMiddlewareCreater>();
                    var chain = new Chain<TParameter, TResult>(creater);
                    foreach (var middleware in MiddlewaresOrder(middlewares))
                    {
                        chain.Add(middleware);
                    }
                    return chain;
                }),
                ServiceLifetime.Transient => services.AddTransient<IChain<TParameter, TResult>>(s =>
                {
                    var creater = s.GetService<IMiddlewareCreater>();
                    var chain = new Chain<TParameter, TResult>(creater);
                    foreach (var middleware in MiddlewaresOrder(middlewares))
                    {
                        chain.Add(middleware);
                    }
                    return chain;
                }),
                ServiceLifetime.Singleton => services.AddSingleton<IChain<TParameter, TResult>>(s =>
                {
                    var creater = s.GetService<IMiddlewareCreater>();
                    var chain = new Chain<TParameter, TResult>(creater);
                    foreach (var middleware in MiddlewaresOrder(middlewares))
                    {
                        chain.Add(middleware);
                    }
                    return chain;
                }),
                _ => throw new NotSupportedException("不支持的注入周期")
            };
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="services"></param>
        /// <param name="middlewares"></param>
        /// <returns></returns>
        public static IServiceCollection AddChainAsyn<TParameter, TResult>(this IServiceCollection services, ServiceLifetime leftTime, params Type[] middlewares)
                where TResult : class, new()
        {
            //中间件注入
            services.InjectMiddleware(leftTime, typeof(DependencyInjectionCreater), middlewares);
            //管道注入
            _ = leftTime switch
            {
                ServiceLifetime.Scoped => services.AddScoped<IChainAsync<TParameter, TResult>>(s =>
                {
                    var creater = s.GetService<IMiddlewareCreater>();
                    var chain = new ChainAsync<TParameter, TResult>(creater);
                    foreach (var middleware in MiddlewaresOrder(middlewares))
                    {
                        chain.Add(middleware);
                    }
                    return chain;
                }),
                ServiceLifetime.Transient => services.AddTransient<IChainAsync<TParameter, TResult>>(s =>
                {
                    var creater = s.GetService<IMiddlewareCreater>();
                    var chain = new ChainAsync<TParameter, TResult>(creater);
                    foreach (var middleware in MiddlewaresOrder(middlewares))
                    {
                        chain.Add(middleware);
                    }
                    return chain;
                }),
                ServiceLifetime.Singleton => services.AddSingleton<IChainAsync<TParameter, TResult>>(s =>
                {
                    var creater = s.GetService<IMiddlewareCreater>();
                    var chain = new ChainAsync<TParameter, TResult>(creater);
                    foreach (var middleware in MiddlewaresOrder(middlewares))
                    {
                        chain.Add(middleware);
                    }
                    return chain;
                }),
                _ => throw new NotSupportedException("不支持的注入周期")
            };
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="services"></param>
        /// <param name="middlewares"></param>
        /// <returns></returns>
        public static IServiceCollection AddPipeline<TParameter>(this IServiceCollection services, ServiceLifetime leftTime, params Type[] middlewares)
        {
            //中间件注入
            services.InjectMiddleware(leftTime, typeof(DependencyInjectionCreater), middlewares);
            //管道注入
            _ = leftTime switch
            {
                ServiceLifetime.Scoped => services.AddScoped<IPipeline<TParameter>>(s =>
                {
                    var creater = s.GetService<IMiddlewareCreater>();
                    var pipeline = new Pipeline<TParameter>(creater);
                    foreach (var middleware in MiddlewaresOrder(middlewares))
                    {
                        pipeline.Add(middleware);
                    }
                    return pipeline;
                }),
                ServiceLifetime.Transient => services.AddTransient<IPipeline<TParameter>>(s =>
                {
                    var creater = s.GetService<IMiddlewareCreater>();
                    var pipeline = new Pipeline<TParameter>(creater);
                    foreach (var middleware in MiddlewaresOrder(middlewares))
                    {
                        pipeline.Add(middleware);
                    }
                    return pipeline;
                }),
                ServiceLifetime.Singleton => services.AddSingleton<IPipeline<TParameter>>(s =>
                {
                    var creater = s.GetService<IMiddlewareCreater>();
                    var pipeline = new Pipeline<TParameter>(creater);
                    foreach (var middleware in MiddlewaresOrder(middlewares))
                    {
                        pipeline.Add(middleware);
                    }
                    return pipeline;
                }),
                _ => throw new NotSupportedException("不支持的注入周期")
            };
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="services"></param>
        /// <param name="middlewares"></param>
        /// <returns></returns>
        public static IServiceCollection AddPipelineAsync<TParameter>(this IServiceCollection services, ServiceLifetime leftTime, params Type[] middlewares)
        {
            //中间件注入
            services.InjectMiddleware(leftTime, typeof(DependencyInjectionCreater), middlewares);
            //管道注入
            _ = leftTime switch
            {
                ServiceLifetime.Scoped => services.AddScoped<IPipelineAsync<TParameter>>(s =>
                {
                    var creater = s.GetService<IMiddlewareCreater>();
                    var pipeline = new PipelineAsync<TParameter>(creater);
                    foreach (var middleware in MiddlewaresOrder(middlewares))
                    {
                        pipeline.Add(middleware);
                    }
                    return pipeline;
                }),
                ServiceLifetime.Transient => services.AddTransient<IPipelineAsync<TParameter>>(s =>
                {
                    var creater = s.GetService<IMiddlewareCreater>();
                    var pipeline = new PipelineAsync<TParameter>(creater);
                    foreach (var middleware in MiddlewaresOrder(middlewares))
                    {
                        pipeline.Add(middleware);
                    }
                    return pipeline;
                }),
                ServiceLifetime.Singleton => services.AddSingleton<IPipelineAsync<TParameter>>(s =>
                {
                    var creater = s.GetService<IMiddlewareCreater>();
                    var pipeline = new PipelineAsync<TParameter>(creater);
                    foreach (var middleware in MiddlewaresOrder(middlewares))
                    {
                        pipeline.Add(middleware);
                    }
                    return pipeline;
                }),
                _ => throw new NotSupportedException("不支持的注入周期")
            };

            return services;
        }
    }
}
