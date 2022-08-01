using Microsoft.Extensions.Configuration;
using NetX.Module;
using System.Reflection;

namespace NetX;

/// <summary>
/// <see cref="WebApplication"/> 方式进行配置
/// </summary>
public sealed class RunOption
{
    /// <summary>
    /// 自定义builder委托<see cref="WebApplicationBuilder"/>
    /// </summary>
    internal Action<WebApplicationBuilder> ActionBuilder;

    /// <summary>
    /// <see cref="WebApplication"/>
    /// </summary>
    internal Action<WebApplication> ActionConfigure;

    /// <summary>
    /// <see cref="ConfigurationManager"/>
    /// </summary>
    internal Action<ConfigurationManager> ActionConfigrationManager;

    /// <summary>
    /// <see cref="WebApplicationOptions"/>
    /// </summary>
    internal WebApplicationOptions Options { get; set; }

    /// <summary>
    /// <see cref="WebApplicationBuilder"/>
    /// </summary>
    internal WebApplicationBuilder Builder { get; set; }
    
    /// <summary>
    /// <see cref="WebApplication"/>
    /// </summary>
    internal WebApplication App { get; set; }

    /// <summary>
    /// 应用服务组件
    /// </summary>
    internal Dictionary<Guid,(ModuleInitializer module, ModuleOptions option)> Modules { get; private set; } = new();

    /// <summary>
    /// 默认配置项
    /// </summary>
    public static RunOption Default { get; } = new RunOption();

    /// <summary>
    /// 内部配置类实例
    /// </summary>
    internal RunOption()
    {
        InitModules();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="option"></param>
    /// <returns></returns>
    public RunOption ConfigOption(WebApplicationOptions options)
    {
        Options = options;
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public RunOption ConfigBuilder(Action<WebApplicationBuilder> builder)
    {
        ActionBuilder = builder;
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="application"></param>
    /// <returns></returns>
    public RunOption ConfigApplication(Action<WebApplication> application)
    {
        ActionConfigure = application;
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configrationManager"></param>
    /// <returns></returns>
    public RunOption ConfigrationManager(Action<ConfigurationManager> configrationManager)
    {
        ActionConfigrationManager = configrationManager;
        return this;
    }

    /// <summary>
    /// 添加应用服务组件
    /// </summary>
    /// <typeparam name="TComponent">组件类型</typeparam>
    /// <returns></returns>
    public RunOption AddModules<TModule>(ModuleOptions moduleOptions)
        where TModule : class, IModule, new()
    {
        AddModule(typeof(TModule), moduleOptions);
        return this;
    }

    /// <summary>
    /// 初始化加载模块:系统模块 && 业务模块
    /// </summary>
    private void InitModules()
    {
        if (null == Modules)
            Modules = new();
        Modules.Clear();
        //添加系统模块
        InitFrameworkModules();
        //添加业务自定义模块
        InitUserModules();
    }

    /// <summary>
    /// 初始化系统模块
    /// </summary>
    private void InitFrameworkModules()
    {
        AddModule(typeof(ServerModuleInitializer), null);
    }

    /// <summary>
    /// 初始化用户自定义模块
    /// </summary>
    private void InitUserModules()
    {
        //加载用户模块
        var modelPath = Path.Combine(AppContext.BaseDirectory, NetXConst.C_MODULE_DIRECTORYNAME);
        if (!Directory.Exists(modelPath))
            Directory.CreateDirectory(modelPath);
        var di = new DirectoryInfo(modelPath);
        di.GetFiles("*.json", SearchOption.AllDirectories).Where(fi => fi.Name.ToLower().Equals(NetXConst.C_MODULE_CINFIGFILENAME))
            ?.ToList().ForEach(fi =>
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(fi.DirectoryName)
                .AddJsonFile(fi.Name)
                .Build();
                var options = builder.Get<ModuleOptions>();
                if (null == options)
                    return;
                //依赖项
                //var v = Assembly.LoadFile(@"D:\Persion\fw\netx\framework\NetX\bin\Debug\net6.0\NetX.dll").GetTypes()
                //        .Where(p => p.IsClass && !p.IsAbstract && p.GetInterfaces().Contains(typeof(IModule)))
                //        .FirstOrDefault();
                //Modules.Add(Assembly.LoadFrom(fi.FullName).GetType()
            });
    }

    /// <summary>
    /// 加载组件模块
    /// </summary>
    /// <typeparam name="TModule"></typeparam>
    private void AddModule(Type modeleType, ModuleOptions moduleOptions)
    {
        var module = (ModuleInitializer)Activator.CreateInstance(modeleType);
        if (null != module && !Modules.ContainsKey(module.Key))
            Modules.Add(module.Key, (module, moduleOptions));
    }
}

