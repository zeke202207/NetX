using System.Runtime.Loader;

namespace NetX.Module;

/// <summary>
/// 独立模块程序集上下文
/// </summary>
public sealed class ModuleAssemblyLoadContext : AssemblyLoadContext
{
    /// <summary>
    /// 模块上下文
    /// </summary>
    public ModuleContext ModuleContext { get; private set; }

    /// <summary>
    /// isCollectible:ture 的时候允许 Unload
    /// </summary>
    /// <param name="moduleName"></param>
    /// <param name="moduleContext"></param>
    public ModuleAssemblyLoadContext(string moduleName, ModuleContext moduleContext)
        : base(moduleName, true)
    {
        ModuleContext = moduleContext;
    }
}
