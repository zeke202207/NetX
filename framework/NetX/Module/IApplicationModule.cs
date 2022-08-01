using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Module
{
    public interface IApplicationModule: IModule
    {
        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context);
    }
}
