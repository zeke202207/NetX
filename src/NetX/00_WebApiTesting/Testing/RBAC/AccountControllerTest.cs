using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace NetX.WebApi.Testing.RBAC
{
    public class AccountControllerTest : TestBase, IClassFixture<IntegrationTestsFactory<Program>> //IClassFixture<TestServerFixture>
    {
        public AccountControllerTest(IntegrationTestsFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task LoginSuccessTest()
        {
            var result = await base.PostRequest("api/account/login", JsonContent.Create(new { username = "zeke", password = "123456" }));
            Assert.True(result.Value<string>("code") == "0");
        }

        [Fact]
        public async Task LoginErrorTest()
        {
            var result = await base.PostRequest("api/account/login", JsonContent.Create(new { username = "zeke1", password = "123456" }));
            Assert.True(result.Value<string>("code") == "-1");
        }

        [Fact]
        public async Task GetUserInfoTest()
        {
            var result = await base.GetResuest("api/account/getuserinfo");
            Assert.True(result.Value<string>("code") == "0");
        }
    }

}
