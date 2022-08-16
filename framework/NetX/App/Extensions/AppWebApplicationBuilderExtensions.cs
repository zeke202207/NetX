using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetX.Module;
using System.Runtime.Loader;

namespace NetX
{
    /// <summary>
    /// 应用程序扩展
    /// </summary>
    public static class AppWebApplicationBuilderExtensions
    {
        /// <summary>
        /// 注入系统服务
        /// </summary>
        /// <param name="webApplicationBuilder"></param>
        /// <param name="env"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static WebApplicationBuilder InjectFrameworkService(
            this WebApplicationBuilder webApplicationBuilder,
            IWebHostEnvironment env,
            IConfiguration config)
        {
            IServiceCollection services = webApplicationBuilder.Services;
            var Initialize = (ModuleInitializer)Activator.CreateInstance(typeof(ServerModuleInitializer));
            AssemblyLoadContext.Default.LoadFromAssemblyName(Initialize.GetType().Assembly.GetName());
            var context = new ModuleContext()
            {
                Configuration = webApplicationBuilder.Configuration,
                Initialize = Initialize,
                ModuleOptions = new ModuleOptions()
                {
                    Id = new Guid("00000000000000000000000000000001")
                }
            };
            Initialize.ConfigureServices(services, env, context);
            InternalApp.FrameworkContextKeyValuePairs.Add(context.ModuleOptions.Id, (Initialize, context));
            return webApplicationBuilder;
        }

        /// <summary>
        /// 注入系统应用
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static WebApplication InjectFrameworkApplication(
            this WebApplication app,
            IWebHostEnvironment env)
        {
            var key = new Guid("00000000000000000000000000000001");
            if (!InternalApp.FrameworkContextKeyValuePairs.ContainsKey(key))
                return app;
            var contents = InternalApp.FrameworkContextKeyValuePairs[key];
            contents.initializer.ConfigureApplication(app, env, contents.context);
            return app;
        }

        /// <summary>
        /// 注入用户模块
        /// </summary>
        /// <param name="webApplicationBuilder"></param>
        /// <param name="options"></param>
        /// <param name="env"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static WebApplicationBuilder InjectUserModulesService(
            this WebApplicationBuilder webApplicationBuilder,
            Dictionary<Guid, ModuleOptions> options,
            IWebHostEnvironment env,
            IConfiguration config)
        {
            foreach (var option in options)
            {
                var contextProvider = new CollectibleAssemblyLoadContextProvider();
                ApplicationPartManager apm = webApplicationBuilder.Services.AddControllers().PartManager;
                IServiceCollection services = webApplicationBuilder.Services;
                var alc = contextProvider.LoadCustomeModule(option.Value, apm, services, env, new ModuleContext() { Configuration = config });
                InternalApp.ModuleCotextKeyValuePairs.Add(option.Value.Id, alc);
            }
            return webApplicationBuilder;
        }

        /// <summary>
        /// 注入用户模块应用
        /// </summary>
        /// <param name="app"></param>
        /// <param name="options"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static WebApplication InjectUserModulesApplication(
            this WebApplication app,
            Dictionary<Guid, ModuleOptions> options,
            IWebHostEnvironment env)
        {
            foreach (var option in options)
            {
                var context = InternalApp.ModuleCotextKeyValuePairs.GetValueOrDefault(option.Value.Id);
                context.ModuleContext.Initialize.ConfigureApplication(app, env, context.ModuleContext);
            }
            return app;
        }
    }
}
