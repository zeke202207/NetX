using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetX.Module;
using System.Reflection;

namespace NetX.TaskScheduling;

/// <summary>
/// 任务调度初始化器
/// </summary>
internal class TaskSchedulingInitializer : ModuleInitializer
{
    public override Guid Key => new Guid(TaskSchedulingConstEnum.C_TASKSCHEDULING_KEY);

    public override ModuleType ModuleType => ModuleType.UserModule;

    public override void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context)
    {

    }

    public override void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context)
    {

    }
}