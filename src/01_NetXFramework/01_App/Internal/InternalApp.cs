using Microsoft.Extensions.Configuration;
using NetX.Module;

namespace NetX.App;

internal class InternalApp
{
    /// <summary>
    /// 根服务
    /// </summary>
    internal static IServiceProvider? RootServices { get; set; }

    internal static IConfiguration? Configuration { get; set; }

    /// <summary>
    /// 用户程序集集合
    /// </summary>
    internal static Dictionary<Guid, ModuleAssemblyLoadContext> ModuleCotextKeyValuePairs = new Dictionary<Guid, ModuleAssemblyLoadContext>();

    /// <summary>
    /// 系统程序集集合
    /// </summary>
    internal static Dictionary<Guid, (ModuleInitializer initializer, ModuleContext context)> FrameworkContextKeyValuePairs = new();

    /// <summary>
    /// 用户组件模块设置项
    /// </summary>
    internal static List<ModuleOptions> UserModeulOptions = new List<ModuleOptions>();

    /// <summary>
    /// 系统组件模块
    /// </summary>
    internal static ModuleOptions FrameworkModuleOptions
    {
        get
        {
            return new ModuleOptions()
            {
                Id = ModuleSetupConst.C_SERVERHOST_MODULE_ID,
                Enabled = true,
                Dependencies = new List<string>(),
                IsSharedAssemblyContext = true,
                Description = ModuleSetupConst.C_MODULE_FRAMEWORK_DESC,
                Name = ModuleSetupConst.C_MODULE_FRAMEWORK_NAME,
                Version = ModuleSetupConst.C_MODULE_FRAMEWORK_VERSION
            };
        }
    }
}
