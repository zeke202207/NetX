using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

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

        /// <summary>
        /// This method gets called by the runtime.
        /// Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env"></param>
        /// <param name="context"></param>
        public abstract void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context);

        /// <summary>
        /// This method gets called by the runtime. 
        /// Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="context"></param>
        public abstract void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context);
    }
}
