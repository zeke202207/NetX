using Microsoft.Extensions.DependencyInjection;
using NetX.Authentication.Core;
using NetX.SharedFramework.ChainPipeline;
using NetX.SharedFramework.ChainPipeline.ChainDataflow;
using NetX.SharedFramework.ChainPipeline.PipelineDataflow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core;

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
            Assert.Equal(result.Count, 3);
        }

        [Fact]
        public async Task ChainAsyncTest()
        {
            var chain = new ChainAsync<DataflowParameterA, DataflowResultA>(_serviceProvider.GetService<IMiddlewareCreater>());
            chain.Add<ChainMiddlewareAsyncA>();
            chain.Add<ChainMiddlewareAsyncB>();
            var result = await chain.ExecuteAsync(new DataflowParameterA() { Id = "zeke" });
            Assert.Equal(result.Count, 2);
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

    public class DataflowParameterA: DataflowParameter
    {
        public string Id { get; set; }
    }

    public class DataflowResultA : DataflowResult
    {
        public int Count { get; set; }

        public bool IsSuccess { get; set; }
    }

    public class ChainMiddlewareA : IChainMiddleware<DataflowParameterA, DataflowResultA>
    {
        public DataflowResultA Run(DataflowParameterA parameter, Func<DataflowParameterA, DataflowResultA> next)
        {
            var result = next(parameter);
            result.Count+=1;
            result.IsSuccess = true;
            return result;
        }
    }

    public class ChainMiddlewareB : IChainMiddleware<DataflowParameterA, DataflowResultA>
    {
        public DataflowResultA Run(DataflowParameterA parameter, Func<DataflowParameterA, DataflowResultA> next)
        {
            var result = next(parameter);
            if (result.IsSuccess)
                result.Count += 1;

            return result;
        }
    }

    public class ChainMiddlewareC : IChainMiddleware<DataflowParameterA, DataflowResultA>
    {
        public DataflowResultA Run(DataflowParameterA parameter, Func<DataflowParameterA, DataflowResultA> next)
        {
            var result = next(parameter);
            if (result.IsSuccess)
                result.Count += 1;

            return result;
        }
    }

    public class ChainMiddlewareAsyncA : IChainMiddlewareAsync<DataflowParameterA, DataflowResultA>
    {
        public async Task<DataflowResultA> RunAsync(DataflowParameterA parameter, Func<DataflowParameterA, Task<DataflowResultA>> next)
        {
            var result = await next(parameter);
            result.Count += 1;
            result.IsSuccess = true;
            return result;
        }
    }

    public class ChainMiddlewareAsyncB : IChainMiddlewareAsync<DataflowParameterA, DataflowResultA>
    {
        public async Task<DataflowResultA> RunAsync(DataflowParameterA parameter, Func<DataflowParameterA, Task<DataflowResultA>> next)
        {
            var result = await next(parameter);
            if (result.IsSuccess)
                result.Count += 1;

            return result;
        }
    }

    public class PipelineMiddlewareA : IPipelineMiddleware<DataflowParameterA>
    {
        public void Run(DataflowParameterA parameter, Action<DataflowParameterA> next)
        {
            next(parameter);
        }
    }

    public class PipelineMiddlewareB : IPipelineMiddleware<DataflowParameterA>
    {
        public void Run(DataflowParameterA parameter, Action<DataflowParameterA> next)
        {
            next(parameter);
        }
    }

    public class PipelineMiddlewareAsyncA : IPilelineMiddlewareAsync<DataflowParameterA>
    {
        public async Task RunAsync(DataflowParameterA parameter, Func<DataflowParameterA, Task>? next)
        {
            await next(parameter);
        }
    }

    public class PipelineMiddlewareAsyncB : IPilelineMiddlewareAsync<DataflowParameterA>
    {
        public async Task RunAsync(DataflowParameterA parameter, Func<DataflowParameterA, Task>? next)
        {
            await next(parameter);
        }
    }
}
