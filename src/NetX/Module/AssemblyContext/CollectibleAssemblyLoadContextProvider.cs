using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.Loader;

namespace NetX.Module
{
    /// <summary>
    /// 可回收程序集provider
    /// </summary>
    internal sealed class CollectibleAssemblyLoadContextProvider
    {
        /// <summary>
        /// 加载用户模块
        /// </summary>
        /// <param name="options"></param>
        /// <param name="apm"></param>
        /// <param name="services"></param>
        /// <param name="env"></param>
        /// <param name="moduleContext"></param>
        /// <returns></returns>
        public ModuleAssemblyLoadContext LoadCustomeModule(
            ModuleOptions options,
            ApplicationPartManager apm,
            IServiceCollection services,
            IWebHostEnvironment env,
            ModuleContext moduleContext)
        {
            var modelPath = Path.Combine(AppContext.BaseDirectory, NetXConst.C_MODULE_DIRECTORYNAME);
            var filePath = Path.Combine(modelPath, Path.GetFileNameWithoutExtension(options.FileName), options.FileName);
            var refPath = Path.Combine(modelPath, NetXConst.C_MODULE_REFDIRECTORYNAME);
            ModuleAssemblyLoadContext context = new ModuleAssemblyLoadContext(filePath, moduleContext);
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                //0. 将程序集装在到context中
                var assembly = context.LoadFromStream(fs);
                //1. 将程序集引用装在到context中
                options.Dependencies?.ForEach(p =>
                {
                    using (var fsRef = new FileStream(p, FileMode.Open))
                    {
                        var assembly = context.LoadFromStream(fsRef);
                    }
                });
                //2. apm装载assemblypart程序集
                AssemblyPart part = new AssemblyPart(assembly);
                apm.ApplicationParts.Add(part);
                //依赖项注入
                var moduleType = assembly.GetTypes().FirstOrDefault(p => typeof(ModuleInitializer).IsAssignableFrom(p));
                context.ModuleContext.Initialize = (ModuleInitializer)Activator.CreateInstance(moduleType);
                context.ModuleContext.Initialize.ConfigureServices(services, env, context.ModuleContext);
                context.ModuleContext.ModuleOptions = options;
            }
            return context;
        }

        /// <summary>
        /// 加载用户模块  AssemblyLoadContext.Default
        /// </summary>
        /// <param name="options"></param>
        /// <param name="apm"></param>
        /// <param name="services"></param>
        /// <param name="env"></param>
        /// <param name="moduleContext"></param>
        /// <returns></returns>
        public ModuleInitializer LoadSharedCustomeModule(
            ModuleOptions options,
            ApplicationPartManager apm,
            IServiceCollection services,
            IWebHostEnvironment env,
            ModuleContext moduleContext)
        {
            var modelPath = Path.Combine(AppContext.BaseDirectory, NetXConst.C_MODULE_DIRECTORYNAME);
            var filePath = Path.Combine(modelPath, Path.GetFileNameWithoutExtension(options.FileName), options.FileName);
            var refPath = Path.Combine(modelPath, NetXConst.C_MODULE_REFDIRECTORYNAME);
            
            using (var fs = new FileStream(filePath, FileMode.Open))
            {
                //0. 将程序集装在到context中
                var assembly = AssemblyLoadContext.Default.LoadFromStream(fs);
                //1. 将程序集引用装在到context中
                options.Dependencies?.ForEach(p =>
                {
                    if (AssemblyLoadContext.Default.Assemblies.ToList().Exists(a => a.GetName().Name.Equals(AssemblyName.GetAssemblyName(p).Name)))
                        return;
                    using (var fsRef = new FileStream(p, FileMode.Open))
                    {
                        var assembly = AssemblyLoadContext.Default.LoadFromStream(fsRef);
                    }
                });
                //2. apm装载assemblypart程序集
                AssemblyPart part = new AssemblyPart(assembly);
                apm.ApplicationParts.Add(part);
                //依赖项注入
                var moduleType = assembly.GetTypes().FirstOrDefault(p => typeof(ModuleInitializer).IsAssignableFrom(p));
                var Initialize = (ModuleInitializer)Activator.CreateInstance(moduleType);
                Initialize.ConfigureServices(services, env, moduleContext);
                return Initialize;
            }
        }
    }
}
