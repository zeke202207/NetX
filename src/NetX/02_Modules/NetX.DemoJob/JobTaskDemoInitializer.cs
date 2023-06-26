using NetX.DatabaseSetup;
using NetX.Module;
using System.Reflection;

namespace NetX.DemoJob
{
    public class JobTaskDemoInitializer : ModuleInitializer
    {
        public override Guid Key => new Guid("90000000000000000000000000000000");

        public override ModuleType ModuleType => ModuleType.UserModule;

        public override void ConfigureApplication(Microsoft.AspNetCore.Builder.IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env, ModuleContext context)
        {
            
        }

        public override void ConfigureServices(Microsoft.Extensions.DependencyInjection.IServiceCollection services, Microsoft.AspNetCore.Hosting.IWebHostEnvironment env, ModuleContext context)
        {
            //CodeFirst
            services.AddMigratorAssembly(new Assembly[] { Assembly.GetExecutingAssembly() }, MigrationSupportDbType.MySql5);
        }
    }
}