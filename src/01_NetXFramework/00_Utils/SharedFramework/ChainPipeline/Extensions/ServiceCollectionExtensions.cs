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
        internal static IServiceCollection InjectMiddleware(this IServiceCollection services, Type middlewareCreater, params Type[] middlewares)
        {
            services.AddScoped(typeof(IMiddlewareCreater), middlewareCreater);
            middlewares.ToList().ForEach(p => services.TryAddScoped(p));
            return services;
        }

        /// <summary>
        /// 中间件排序
        /// </summary>
        /// <param name="middlewares"></param>
        /// <returns></returns>
        private static IEnumerable<Type> MiddlewaresOrder(params Type[] middlewares)
        {
            foreach (var middleware in middlewares.OrderByDescending(p => p.GetCustomAttribute<ChainPipelineAttribute>()?.Order))
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
        public static IServiceCollection AddChain<TParameter, TResult>(this IServiceCollection services, params Type[] middlewares)
                where TResult : class, new()
        {
            //中间件注入
            services.InjectMiddleware(typeof(DependencyInjectionCreater), middlewares);
            //管道注入
            services.AddTransient<IChain<TParameter, TResult>>(s =>
            {
                var creater = s.GetService<IMiddlewareCreater>();
                var chain = new Chain<TParameter, TResult>(creater);
                foreach (var middleware in MiddlewaresOrder(middlewares))
                {
                    chain.Add(middleware);
                }
                return chain;
            });
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
        public static IServiceCollection AddChainAsyn<TParameter, TResult>(this IServiceCollection services, params Type[] middlewares)
                where TResult : class, new()
        {
            //中间件注入
            services.InjectMiddleware(typeof(DependencyInjectionCreater), middlewares);
            //管道注入
            services.AddTransient<IChainAsync<TParameter, TResult>>(s =>
            {
                var creater = s.GetService<IMiddlewareCreater>();
                var chain = new ChainAsync<TParameter, TResult>(creater);
                foreach (var middleware in MiddlewaresOrder(middlewares))
                {
                    chain.Add(middleware);
                }
                return chain;
            });
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="services"></param>
        /// <param name="middlewares"></param>
        /// <returns></returns>
        public static IServiceCollection AddPipeline<TParameter>(this IServiceCollection services, params Type[] middlewares)
        {
            //中间件注入
            services.InjectMiddleware(typeof(DependencyInjectionCreater), middlewares);
            //管道注入
            services.AddTransient<IPipeline<TParameter>>(s =>
            {
                var creater = s.GetService<IMiddlewareCreater>();
                var pipeline = new Pipeline<TParameter>(creater);
                foreach (var middleware in MiddlewaresOrder(middlewares))
                {
                    pipeline.Add(middleware);
                }
                return pipeline;
            });
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TParameter"></typeparam>
        /// <param name="services"></param>
        /// <param name="middlewares"></param>
        /// <returns></returns>
        public static IServiceCollection AddPipelineAsync<TParameter>(this IServiceCollection services, params Type[] middlewares)
        {
            //中间件注入
            services.InjectMiddleware(typeof(DependencyInjectionCreater), middlewares);
            //管道注入
            services.AddTransient<IPipelineAsync<TParameter>>(s =>
            {
                var creater = s.GetService<IMiddlewareCreater>();
                var pipeline = new PipelineAsync<TParameter>(creater);
                foreach (var middleware in MiddlewaresOrder(middlewares))
                {
                    pipeline.Add(middleware);
                }
                return pipeline;
            });
            return services;
        }
    }
}
