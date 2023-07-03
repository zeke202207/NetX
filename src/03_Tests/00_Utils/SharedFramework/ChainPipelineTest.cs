using Microsoft.Extensions.DependencyInjection;
using NetX.Authentication.Core;
using NetX.SharedFramework.ChainPipeline;
using NetX.SharedFramework.ChainPipeline.ChainDataflow;
using NetX.SharedFramework.ChainPipeline.PipelineDataflow;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tests._00_Utils.SharedFramework
{
    public class ChainPipelineTest
    {
        private readonly IServiceProvider _serviceProvider;

        public ChainPipelineTest()
        {
            _serviceProvider = IocInject();
        }

        private IServiceProvider IocInject()
        {
            IServiceCollection services = new ServiceCollection();
            //注入
            services.AddTransient<IMiddlewareCreater, ActivatorMiddlewareCreater>();

            //构建容器
            return services.BuildServiceProvider();
        }

        [Fact]
        public void ChainTest()
        {
            var chain = new Chain<DataflowParameterA, DataflowResultA>(_serviceProvider.GetService<IMiddlewareCreater>());
            chain.Add<ChainMiddlewareA>();
            chain.Add<ChainMiddlewareB>();
            chain.Add<ChainMiddlewareC>();
            var result = chain.Execute(new DataflowParameterA() { Id = "zeke" });
            Assert.Equal(result.Result.Count, 3);
        }

        [Fact]
        public async Task ChainAsyncTest()
        {
            var chain = new ChainAsync<DataflowParameterA, DataflowResultA>(_serviceProvider.GetService<IMiddlewareCreater>());
            chain.Add<ChainMiddlewareAsyncA>();
            chain.Add<ChainMiddlewareAsyncB>();
            var result = await chain.ExecuteAsync(new DataflowParameterA() { Id = "zeke" });
            Assert.Equal(result.Result.Count, 2);
        }

        [Fact]
        public void PipelineTest()
        {
            var pipeline = new Pipeline<DataflowParameterA>(_serviceProvider.GetService<IMiddlewareCreater>());
            pipeline.Add<PipelineMiddlewareA>();
            pipeline.Add<PipelineMiddlewareB>();
            pipeline.Execute(new DataflowParameterA() { Id = "zeke" });
        }

        [Fact]
        public async Task PipelineAsyncTest()
        {
            var pipeline = new PipelineAsync<DataflowParameterA>(_serviceProvider.GetService<IMiddlewareCreater>());
            pipeline.Add<PipelineMiddlewareAsyncA>();
            pipeline.Add<PipelineMiddlewareAsyncB>();
            await pipeline.ExecuteAsync(new DataflowParameterA() { Id = "zeke" });
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

    public class ChainMiddlewareA : IChainMiddleware<DataflowParameterA, DataflowResultA>
    {
        public ReponseContext<DataflowResultA> Run(RequestContext<DataflowParameterA> parameter, Func<RequestContext<DataflowParameterA>, ReponseContext<DataflowResultA>> next)
        {
            var result = next(parameter);
            result.Result.Count+=1;
            result.Result.IsSuccess = true;
            return result;
        }
    }

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

    public class ChainMiddlewareC : IChainMiddleware<DataflowParameterA, DataflowResultA>
    {
        public ReponseContext<DataflowResultA> Run(RequestContext<DataflowParameterA> parameter, Func<RequestContext<DataflowParameterA>, ReponseContext<DataflowResultA>> next)
        {
            var result = next(parameter);
            if (result.Result.IsSuccess)
                result.Result.Count += 1;

            return result;
        }
    }

    public class ChainMiddlewareAsyncA : IChainMiddlewareAsync<DataflowParameterA, DataflowResultA>
    {
        public async Task<ReponseContext<DataflowResultA>> RunAsync(RequestContext<DataflowParameterA> parameter, Func<RequestContext<DataflowParameterA>, Task<ReponseContext<DataflowResultA>>> next)
        {
            var result = await next(parameter);
            result.Result.Count += 1;
            result.Result.IsSuccess = true;
            return result;
        }
    }

    public class ChainMiddlewareAsyncB : IChainMiddlewareAsync<DataflowParameterA, DataflowResultA>
    {
        public async Task<ReponseContext<DataflowResultA>> RunAsync(RequestContext<DataflowParameterA> parameter, Func<RequestContext<DataflowParameterA>, Task<ReponseContext<DataflowResultA>>> next)
        {
            var result = await next(parameter);
            if (result.Result.IsSuccess)
                result.Result.Count += 1;

            return result;
        }
    }

    public class PipelineMiddlewareA : IPipelineMiddleware<DataflowParameterA>
    {
        public void Run(RequestContext<DataflowParameterA> parameter, Action<RequestContext<DataflowParameterA>> next)
        {
            next(parameter);
        }
    }

    public class PipelineMiddlewareB : IPipelineMiddleware<DataflowParameterA>
    {
        public void Run(RequestContext<DataflowParameterA> parameter, Action<RequestContext<DataflowParameterA>> next)
        {
            next(parameter);
        }
    }

    public class PipelineMiddlewareAsyncA : IPilelineMiddlewareAsync<DataflowParameterA>
    {
        public async Task RunAsync(RequestContext<DataflowParameterA> parameter, Func<RequestContext<DataflowParameterA>, Task>? next)
        {
            await next(parameter);
        }
    }

    public class PipelineMiddlewareAsyncB : IPilelineMiddlewareAsync<DataflowParameterA>
    {
        public async Task RunAsync(RequestContext<DataflowParameterA> parameter, Func<RequestContext<DataflowParameterA>, Task>? next)
        {
            await next(parameter);
        }
    }
}
