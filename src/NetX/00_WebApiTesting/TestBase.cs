using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NetX.WebApi.Testing
{
    public abstract class TestBase
    {
        private readonly Microsoft.AspNetCore.TestHost.TestServer _server;

        protected IntegrationTestsConfiguration Configuration { get; }
        protected HttpClient Client { get; }

        public TestBase(IntegrationTestsFactory<Program> factory)
        {
            _server = factory.Server;

            Client = factory.CreateClient();
            Configuration = factory.IntegrationTestsConfiguration;
            AuthorizeClientWithFakeJwt(Client, factory.IntegrationTestsConfiguration);
        }

        private void AuthorizeClientWithFakeJwt(HttpClient client, IntegrationTestsConfiguration configuration)
        {
            //dynamic bearer = new ExpandoObject();
            //bearer.sub = configuration.UserIdentifier;
            //bearer.oid = configuration.UserIdentifier;
            //bearer.email = configuration.UserEmail;
            //bearer.name = configuration.UserName;

            //client.SetFakeBearerToken((object)bearer);
        }

        //protected async Task ArrangeDatabaseAsync(Action<DataContext> action)
        //{
        //    await using var context = _server.Services.GetService<DataContext>();

        //    action(context);

        //    context.SaveChanges();
        //}

        protected async Task<JObject> Request(string api, JsonContent jsonParam)
        {
            var response = await Client.PostAsync(api, jsonParam);
            var strResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JObject>(strResult);
        }
    }
}
