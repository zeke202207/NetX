using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NetX.WebApi.Testing
{
    public abstract class TestBase
    {
        private readonly Microsoft.AspNetCore.TestHost.TestServer _server;

        protected HttpClient _client { get; }

        public TestBase(IntegrationTestsFactory<Program> factory)
        {
            _server = factory.Server;
            _client = factory.CreateClient();
            GetToken().GetAwaiter().GetResult();
        }

        private async Task GetToken()
        {
            var result = await PostRequest("api/account/login", JsonContent.Create(new { username = "zeke", password = "123456" }));
            if (result.Value<string>("code") == "0")
                await SetToken(result.GetValue("result").Value<string>("token"));
        }

        protected async Task SetToken(string token)
        {
            _client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            await Task.CompletedTask;
        }

        protected async Task<JObject> PostRequest(string api, JsonContent jsonParam)
        {
            var response = await _client.PostAsync(api, jsonParam);
            var strResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JObject>(strResult);
        }

        protected async Task<JObject> GetResuest(string api)
        {
            var response = await _client.GetAsync(api);
            var strResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<JObject>(strResult);
        }
    }
}
