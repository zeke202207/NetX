using System.Runtime.Loader;

namespace NetX.Module;

public sealed class ModuleAssemblyLoadContext : AssemblyLoadContext
{
    public ModuleContext ModuleContext { get; private set; }

    /// <summary>
    /// isCollectible:ture 的时候允许 Unload
    /// </summary>
    /// <param name="moduleName"></param>
    public ModuleAssemblyLoadContext(string moduleName, ModuleContext moduleContext)
        : base(true)
    {
        ModuleContext = moduleContext;
    }
}
