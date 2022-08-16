using Microsoft.Extensions.Configuration;
using NetX.Module;

namespace NetX;

internal class InternalApp
{
    /// <summary>
    /// 根服务
    /// </summary>
    internal static IServiceProvider RootServices { get; set; }

    internal static IConfiguration Configuration { get; set; }

    /// <summary>
    /// 用户程序集集合
    /// </summary>
    internal static Dictionary<Guid, ModuleAssemblyLoadContext> ModuleCotextKeyValuePairs = new Dictionary<Guid, ModuleAssemblyLoadContext>();

    /// <summary>
    /// 系统程序集集合
    /// </summary>
    internal static Dictionary<Guid, (ModuleInitializer initializer, ModuleContext context)> FrameworkContextKeyValuePairs = new();
}
