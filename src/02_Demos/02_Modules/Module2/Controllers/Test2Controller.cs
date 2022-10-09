using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetX.App;
using NetX.Authentication;
using NetX.Authentication.Core;
using NetX.DatabaseSetup;
using NetX.EventBus;
using NetX.Swagger;
using NetX.Tenants;
using System.ComponentModel;

namespace Module2.Controllers
{

    [ApiControllerDescriptionAttribute("Module2", Description = "测试控制器描述Module2", HeaderKeys = new string[] { "version","x-test1","x-test2" })]
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class Test2Controller : ApiPermissionController
    {
        private readonly ILogger<Test2Controller> _logger;
        private readonly ITest _test;
        private readonly IEventPublisher _publisher;
        private readonly ITenantAccessor<Tenant> _accessor;
        private readonly IOptions<CookiePolicyOptions> _options;
        private readonly ILoginHandler _login;
        private readonly MigrationService _migrationService;
        private readonly IFreeSql _fsql;

        public Test2Controller(
            ILoginHandler login,
            MigrationService migrationService,
            ILogger<Test2Controller> logger, 
            ITest test,
            IEventPublisher publisher,
            ITenantAccessor<Tenant> accessor,
            IOptions<CookiePolicyOptions> c,
            IFreeSql fsql)
        {
            _logger = logger;
            _test = test;
            _publisher = publisher;
            _accessor = accessor;
            _options = c;
            _login = login;
            _migrationService = migrationService;
            this._fsql = fsql;
        }

        [HttpGet(Name = "zeke2")]
        public async Task<IEnumerable<string>> Get()
        {
            _logger.LogInformation("hi,zeke,this is a log");
            _logger.LogError(new NotSupportedException("i am a not supported exception"), "this is a message");

            //var info1 = HttpContext.GetTenant();
            var info = _accessor.Tenant;


            _publisher.PublishAsync(new EventSource("zeke", "hi,zeke"), new CancellationToken()).GetAwaiter().GetResult();
            var v = Newtonsoft.Json.JsonConvert.SerializeObject("{}");
            //return Enumerable.Range(1, 5).Select(index => index.ToString())
            //.ToArray();
            return new List<string>() { typeof(Newtonsoft.Json.JsonConvert).Assembly.GetName().Version.ToString() };
        }

        /// <summary>
        /// 获取访问Token
        /// </summary>
        /// <returns></returns>
        [ApiActionDescriptionAttribute("获取访问Token接口")]
        [NoPermission]
        [HttpPost]
        public ActionResult GetToken(LoginModel model)
        {
            var result = _fsql.Queryable<Log1>().ToList();
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
            //_migrationService.SetupDatabase();
            return Task.FromResult("hi,zeke");
        }
    }

    public class LoginModel
    {
        public string password { get; set; }
        public string username { get; set; }
    }

    public class Log1
    {
        public int Id { get; set; }

        public string Text { get; set; }
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
