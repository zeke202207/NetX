using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetX.Module;
using System;
using System.Collections.Generic;
using System.Linq;
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
            IServiceCollection services,
            IWebHostEnvironment env)
        {
            //TODO:自定义模块加载到程序集中

            return webApplicationBuilder;
        }
    }
}
