using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace NetX.Module
{
    public interface IServiceModule : IModule
    {
        /// <summary>
        /// 注入服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="env">环境变量</param>
        void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context);
    }
}
