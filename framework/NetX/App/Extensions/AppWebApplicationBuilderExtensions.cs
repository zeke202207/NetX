using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetX.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace NetX
{
    public static class AppWebApplicationBuilderExtensions
    {
        /// <summary>
        /// 用户自定义模块注入
        /// </summary>
        /// <param name="webApplicationBuilder"></param>
        /// <returns></returns>
        public static WebApplicationBuilder InjectUserModules(this WebApplicationBuilder webApplicationBuilder,
            IMvcBuilder builder,
            IServiceCollection services,
            IWebHostEnvironment env,
            ModuleOptions options)
        {
            var contextProvider = new CollectibleAssemblyLoadContextProvider();
            var alc = contextProvider.Get(options, builder);
            //自定义模块加载到程序集中
            AssemblyLoadContextResoving();
            return webApplicationBuilder;
        }

        /// <summary>
        /// 插件加载到程序集中
        /// </summary>
        private static void AssemblyLoadContextResoving()
        {
            //TODO:动态加载依赖程序集
            AssemblyLoadContext.Default.Resolving += (content, assembly) =>
            {
                return null;
            };
        }
    }
}
