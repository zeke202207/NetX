using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetX.Common;
using NetX.DatabaseSetup;
using NetX.Module;
using NetX.SystemManager.Profiles;
using System.Reflection;

namespace NetX.SystemManager;

internal class SystemManagerInitializer : ModuleInitializer
{
    public override Guid Key => new Guid("10000000000000000000000000000001");

    public override ModuleType ModuleType => ModuleType.UserModule;

    public override void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context)
    {

    }

    public override void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddSingleton<IEncryption, MD5>();
        //code first
        services.AddMigratorAssembly(new Assembly[] { Assembly.GetExecutingAssembly() });
    }
}
