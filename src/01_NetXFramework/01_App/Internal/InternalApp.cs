using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetX.Module;
using System.Collections.Concurrent;

namespace NetX.App;

internal class InternalApp
{
    /// <summary>
    /// 根服务
    /// </summary>
    internal static IServiceProvider? RootServices { get; set; }

    internal static IServiceCollection Services { get; set; }

    internal static IConfiguration? Configuration { get; set; }

    /// <summary>
    /// 系统模块、自定义模块上下文缓存集合
    /// </summary>
    internal static ConcurrentDictionary<Guid, ModuleContext> ModuleContexts = new();

    /// <summary>
    /// 用户组件模块设置项
    /// </summary>
    internal static ConcurrentBag<ModuleOptions> UserModeulOptions = new ();

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
