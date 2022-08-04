using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class Test2Controller : ControllerBase
    {
        private readonly ILogger<Test2Controller> _logger;
        private readonly ITest _test;

        public Test2Controller(/*ILogger<TestController> logger,*/ITest test)
        {
            //_logger = logger;
            _test = test;
        }

        [HttpGet(Name = "zeke2")]
        public IEnumerable<string> Get()
        {
            var v = Newtonsoft.Json.JsonConvert.SerializeObject("{}");
            //return Enumerable.Range(1, 5).Select(index => index.ToString())
            //.ToArray();
            return new List<string>() { typeof(Newtonsoft.Json.JsonConvert).Assembly.GetName().Version.ToString() };
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
