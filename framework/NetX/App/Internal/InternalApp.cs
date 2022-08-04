using NetX.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX;

internal class InternalApp
{
    /// <summary>
    /// 根服务
    /// </summary>
    internal static IServiceProvider RootServices { get; set; }

    /// <summary>
    /// 用户程序集集合
    /// </summary>
    internal static Dictionary<Guid, ModuleAssemblyLoadContext> ModuleCotextKeyValuePairs = new Dictionary<Guid, ModuleAssemblyLoadContext>();

    /// <summary>
    /// 系统程序集集合
    /// </summary>
    internal static Dictionary<Guid, (ModuleInitializer initializer, ModuleContext context)> FrameworkContextKeyValuePairs = new();
}
