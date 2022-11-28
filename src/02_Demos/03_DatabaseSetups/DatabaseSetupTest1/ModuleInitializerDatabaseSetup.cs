using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetX.DatabaseSetup;
using NetX.Module;
using System.Reflection;

namespace DatabaseSetupTest
{
    public class ModuleInitializerDatabaseSetup : ModuleInitializer
    {
        public override Guid Key => new Guid("10000000000000000000000000000001");

        public override ModuleType ModuleType => ModuleType.UserModule;

        public override void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context)
        {

        }

        public override void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context)
        {
            services.AddMigratorAssembly(new Assembly[] { Assembly.GetExecutingAssembly() }, MigrationSupportDbType.MySql5);
        }
    }
}
