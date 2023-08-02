using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace NetX.Module;

/// <summary>
/// 模块上下文 
/// </summary>
public sealed class ModuleContext
{
    /// <summary>
    /// 是否状态完成
    /// </summary>
    public bool IsLoaded { get; set; } = false;

    /// <summary>
    /// 配置项
    /// </summary>
    public IConfiguration? Configuration { get; set; }

    /// <summary>
    /// 模块配置项 <see cref="ModuleOptions"/>
    /// </summary>
    public ModuleOptions? ModuleOptions { get; set; }

    /// <summary>
    /// 模块初始化器 <see cref="ModuleInitializer"/>
    /// </summary>
    public ModuleInitializer? Initialize { get; set; }

    /// <summary>
    /// 配置应用程序
    /// </summary>
    public Action<ModuleContext ,IApplicationBuilder, IWebHostEnvironment> ConfigApplication;
}
