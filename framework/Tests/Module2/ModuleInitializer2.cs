using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Module2.Controllers;
using NetX;
using NetX.Module;

namespace Module2;

public class ModuleInitializer2 : ModuleInitializer
{
    public ModuleInitializer2()
    {
    }

    public override Guid Key => new Guid("00000000000000000000000000000003");
    public override ModuleType ModuleType => ModuleType.UserModule;

    public override void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context)
    {
        
    }

    public override void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context)
    {
        services.AddScoped<ITest, MyTest>();
    }
}