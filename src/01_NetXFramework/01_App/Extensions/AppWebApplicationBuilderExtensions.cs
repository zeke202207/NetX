using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetX.Module;
using System.Runtime.Loader;

namespace NetX.App
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
            var objectInitialize = Activator.CreateInstance(typeof(ServerModuleInitializer));
            if (null == objectInitialize)
                return webApplicationBuilder;
            var Initialize = (ModuleInitializer)objectInitialize;
            AssemblyLoadContext.Default.LoadFromAssemblyName(Initialize.GetType().Assembly.GetName());
            var context = new ModuleContext()
            {
                Configuration = webApplicationBuilder.Configuration,
                Initialize = Initialize,
                ModuleOptions = new ModuleOptions() { Id = ModuleSetupConst.C_SERVERHOST_MODULE_ID }
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
            if (!InternalApp.FrameworkContextKeyValuePairs.ContainsKey(ModuleSetupConst.C_SERVERHOST_MODULE_ID))
                return app;
            var contents = InternalApp.FrameworkContextKeyValuePairs[ModuleSetupConst.C_SERVERHOST_MODULE_ID];
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
                ApplicationPartManager apm = webApplicationBuilder.Services.AddControllers().PartManager;
                IServiceCollection services = webApplicationBuilder.Services;
                var contextProvider = new CollectibleAssemblyLoadContextProvider();
                ModuleContext context = new ModuleContext() { Configuration = config, ModuleOptions = option.Value };
                if (option.Value.IsSharedAssemblyContext)
                {
                    var Initialize = contextProvider.LoadSharedCustomeModule(option.Value, apm, services, env, context);
                    InternalApp.FrameworkContextKeyValuePairs.Add(context.ModuleOptions.Id, (Initialize, context));
                    //统一注入 
                    services.AddServicesFromAssembly(Initialize.GetType().Assembly);
                }
                else
                {
                    var alc = contextProvider.LoadCustomeModule(option.Value, apm, services, env, context);
                    InternalApp.ModuleCotextKeyValuePairs.Add(option.Value.Id, alc);
                    //统一注入 
                    //services.AddServicesFromAssembly(alc.Assemblies);
                }
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
                if (option.Value.IsSharedAssemblyContext)
                {
                    var contents = InternalApp.FrameworkContextKeyValuePairs[option.Value.Id];
                    contents.initializer.ConfigureApplication(app, env, contents.context);
                }
                else
                {
                    var context = InternalApp.ModuleCotextKeyValuePairs.GetValueOrDefault(option.Value.Id);
                    context?.ModuleContext.Initialize?.ConfigureApplication(app, env, context.ModuleContext);
                }
            }
            return app;
        }
    }
}
