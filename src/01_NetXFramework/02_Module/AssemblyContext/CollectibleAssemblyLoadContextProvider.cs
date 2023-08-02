using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Runtime.Loader;

namespace NetX.Module;

/// <summary>
/// 可回收程序集provider
/// </summary>
public sealed class CollectibleAssemblyLoadContextProvider
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
    public ModuleInitializer LoadCustomModule(
        ModuleOptions options,
        ApplicationPartManager apm,
        IServiceCollection services,
        IWebHostEnvironment env,
        ModuleContext moduleContext)
    {
        (string modulePath, string filePath, string refPath) paths = GetPaths(options);
        ModuleAssemblyLoadContext context = new ModuleAssemblyLoadContext(options.Name + options.Version, moduleContext);
        using (var fs = new FileStream(paths.filePath, FileMode.Open))
        {
            //0. 将程序集装在到context中
            var assembly = context.LoadFromStream(fs);
            AssemblyLoadContextManager.Instance.AddOrUpdate(assembly.GetName().Name, context);
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
            if(null != moduleType)
            {
                var moduleInitalizerInstance = Activator.CreateInstance(moduleType);
                if( moduleInitalizerInstance is ModuleInitializer)
                {
                    if (!services.IsReadOnly)
                    {
                        context.ModuleContext.Initialize = moduleInitalizerInstance as ModuleInitializer;
                        context.ModuleContext.Initialize?.ConfigureServices(services, env, context.ModuleContext);
                        context.ModuleContext.IsLoaded = true;
                    }
                }
            }
            context.ModuleContext.ModuleOptions = options;
        }
        return context.ModuleContext.Initialize;
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
    public ModuleInitializer LoadSharedCustomModule(
        ModuleOptions options,
        ApplicationPartManager apm,
        IServiceCollection services,
        IWebHostEnvironment env,
        ModuleContext moduleContext)
    {
        (string modulePath, string filePath, string refPath) paths = GetPaths(options);
        using (var fs = new FileStream(paths.filePath, FileMode.Open))
        {
            //0. 将程序集装在到context中
            var assembly = ModuleAssemblyLoadContext.Default.LoadFromStream(fs);
            AssemblyLoadContextManager.Instance.AddOrUpdate(assembly.GetName().Name, ModuleAssemblyLoadContext.Default);
            //1. 将程序集引用装在到context中
            options.Dependencies.ForEach(p =>
            {
                if (ModuleAssemblyLoadContext.Default.Assemblies.ToList().Exists(a =>
                {
                    var aName = a.GetName().Name;
                    if (!string.IsNullOrWhiteSpace(aName))
                        return aName.Equals(AssemblyName.GetAssemblyName(p).Name);
                    else
                        return false;
                }))
                {
                    return;
                }
                using (var fsRef = new FileStream(p, FileMode.Open))
                {
                    ModuleAssemblyLoadContext.Default.LoadFromStream(fsRef);
                }
            });
            //2. apm装载assemblypart程序集
            AssemblyPart part = new AssemblyPart(assembly);
            apm.ApplicationParts.Add(part);
            //依赖项注入
            var moduleType = assembly.GetTypes().FirstOrDefault(p => typeof(ModuleInitializer).IsAssignableFrom(p));
            ModuleInitializer? initialize = null;
            if (null != moduleType)
            {
                var moduleInitializerInstance = Activator.CreateInstance(moduleType);
                if(moduleInitializerInstance is ModuleInitializer)
                {
                    initialize = moduleInitializerInstance as ModuleInitializer;
                    if (!services.IsReadOnly)
                    {
                        initialize?.ConfigureServices(services, env, moduleContext);
                        moduleContext.IsLoaded = true;
                    }
                }
            }
            return initialize;
        }
    }

    /// <summary>
    /// 获取模块相关目录
    /// </summary>
    /// <param name="options">模块配置项</param>
    /// <returns></returns>
    private (string modelPath,string filePath,string refPath) GetPaths(ModuleOptions options)
    {
        var modelPath = Path.Combine(AppContext.BaseDirectory, ModuleSetupConst.C_MODULE_DIRECTORYNAME);
        var filePath = Path.Combine(modelPath, Path.GetFileNameWithoutExtension(options.Name), options.FileName);
        var refPath = Path.Combine(modelPath, ModuleSetupConst.C_MODULE_REFDIRECTORYNAME);
        return (modelPath: modelPath, filePath: filePath, refPath: refPath);
    }
}
