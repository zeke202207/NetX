using Castle.Core.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.WebApi.Testing
{
    public class IntegrationTestsFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Tests");
            builder.ConfigureServices(services =>
            {
                InitIntegrationTestsConfiguration(services);
                //services.OverrideDbWithInMemoryDb<TStartup>(IntegrationTestsConfiguration);
                //services.OverrideAuthentication();
            });
        }

        private void InitIntegrationTestsConfiguration(IServiceCollection services)
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        }
    }
}
