using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Module;

internal sealed class ModuleAssemblyLoadContext : AssemblyLoadContext
{
    /// <summary>
    /// isCollectible:ture 的时候允许 Unload
    /// </summary>
    /// <param name="moduleName"></param>
    public ModuleAssemblyLoadContext(string moduleName) 
        : base(true)
    {

    }

    protected override Assembly Load(AssemblyName assemblyName)
    {
        return null;
    }
}
