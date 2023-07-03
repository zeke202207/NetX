using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetX.Authentication;
using NetX.Authentication.Core;
using NetX.SharedFramework.ChainPipeline;
using NetX.SharedFramework.ChainPipeline.ChainDataflow;
using NetX.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1.Controllers
{
    [ApiControllerDescriptionAttribute("Module1",Description ="测试控制器描述")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;
        private readonly ITest _test;
        private readonly IChain<DataflowParameterA, DataflowResultA> _chain;

        public TestController(/*ILogger<TestController> logger,ITest test*/ IChain<DataflowParameterA, DataflowResultA> chain)
        {
            //_logger = logger;
            //_test = test;
            _chain = chain;
        }

        [NoPermission]
        [HttpGet(Name = "zeke")]
        public IEnumerable<string> Get()
        {
            //var v = Newtonsoft.Json.JsonConvert.SerializeObject("{}");
            //return Enumerable.Range(1, 5).Select(index => index.ToString())
            //.ToArray();
            var result = _chain.Execute(new DataflowParameterA() { Id = "zeke" });
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




    public class DataflowParameterA
    {
        public string Id { get; set; }
    }

    public class DataflowResultA 
    {
        public int Count { get; set; }

        public bool IsSuccess { get; set; }
    }

    [ChainPipeline(1)]
    public class ChainMiddlewareA : IChainMiddleware<DataflowParameterA, DataflowResultA>
    {
        public ReponseContext<DataflowResultA> Run(RequestContext<DataflowParameterA> parameter, Func<RequestContext<DataflowParameterA>, ReponseContext<DataflowResultA>> next)
        {
            var result = next(parameter);
            result.Result.Count += 1;
            result.Result.IsSuccess = true;
            return result;
        }
    }

    [ChainPipeline(2)]
    public class ChainMiddlewareB : IChainMiddleware<DataflowParameterA, DataflowResultA>
    {
        public ReponseContext<DataflowResultA> Run(RequestContext<DataflowParameterA> parameter, Func<RequestContext<DataflowParameterA>, ReponseContext<DataflowResultA>> next)
        {
            var result = next(parameter);
            if (result.Result.IsSuccess)
                result.Result.Count += 1;
            return result;
        }
    }

    [ChainPipeline(0)]
    public class ChainMiddlewareC : IChainMiddleware<DataflowParameterA, DataflowResultA>
    {
        public ReponseContext<DataflowResultA> Run(RequestContext<DataflowParameterA> parameter, Func<RequestContext<DataflowParameterA>, ReponseContext<DataflowResultA>> next)
        {
            parameter.Cts.Cancel();
            var result = next(parameter);
            if (result.Result.IsSuccess)
                result.Result.Count += 1;

            return result;
        }
    }
}
