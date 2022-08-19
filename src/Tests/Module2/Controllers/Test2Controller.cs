using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetX;
using NetX.Authentication;
using NetX.DatabaseSetup;
using NetX.EventBus;
using NetX.MutilTenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Module2.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Test2Controller : ApiPermissionController
    {
        private readonly ILogger<Test2Controller> _logger;
        private readonly ITest _test;
        private readonly IEventPublisher _publisher;
        private readonly ITenantAccessor<Tenant> _accessor;
        private readonly IOptions<CookiePolicyOptions> _options;
        private readonly ILoginHandler _login;
        private readonly MigrationService _migrationService;

        public Test2Controller(
            ILoginHandler login,
            MigrationService migrationService,
            /*ILogger<TestController> logger,*/ITest test,IEventPublisher publisher, ITenantAccessor<Tenant> accessor, IOptions<CookiePolicyOptions> c)
        {
            //_logger = logger;
            _test = test;
            _publisher = publisher;
            _accessor = accessor;
            _options = c;
            _login = login;
            _migrationService = migrationService;
        }

        [HttpGet(Name = "zeke2")]
        public async Task<IEnumerable<string>> Get()
        {
            //var info1 = HttpContext.GetTenant();
            var info = _accessor.Tenant;


            _publisher.PublishAsync(new EventSource("zeke","hi,zeke"),new CancellationToken()).GetAwaiter().GetResult();
            var v = Newtonsoft.Json.JsonConvert.SerializeObject("{}");
            //return Enumerable.Range(1, 5).Select(index => index.ToString())
            //.ToArray();
            return new List<string>() { typeof(Newtonsoft.Json.JsonConvert).Assembly.GetName().Version.ToString() };
        }

        [NoPermission]
        [HttpGet]
        public ActionResult GetToken()
        {
            return new JsonResult(_login.Handle(new ClaimModel()
            {
                UserId = "12345",
                LoginName = "abc",
                DisplayName = "zeke"
            }, String.Empty));
            //return Task.FromResult(Newtonsoft.Json.JsonConvert.SerializeObject( _login.Hand(claims.ToArray(), String.Empty)));
        }

        [HttpGet]
        public Task<string> Test()
        {
            _migrationService.SetupDatabase();
            return Task.FromResult("hi,zeke");
        }
    }

    public interface ITest
    {
        string Test();
    }

    public class MyTest : ITest
    {
        public string Test()
        {
            return "zeke";
        }
    }
}
