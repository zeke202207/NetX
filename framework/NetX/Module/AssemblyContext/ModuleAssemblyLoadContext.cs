using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

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
