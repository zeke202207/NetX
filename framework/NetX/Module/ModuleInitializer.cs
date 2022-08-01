using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetX.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Module
{
    /// <summary>
    /// Serve 组件应用服务组件
    /// </summary>
    public abstract class ModuleInitializer : IServiceModule, IApplicationModule
    {
        /// <summary>
        /// 模块唯一标识
        /// </summary>
        public abstract Guid Key { get; }

        /// <summary>
        /// 模块类型
        /// </summary>
        public abstract ModuleType ModuleType { get; }

        public abstract void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context);

        public abstract void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context);
    }
}
