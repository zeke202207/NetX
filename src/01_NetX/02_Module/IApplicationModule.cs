using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace NetX.Module
{
    public interface IApplicationModule : IModule
    {
        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context);
    }
}
