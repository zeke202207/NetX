using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Module1.Controllers;
using NetX;
using NetX.EventBus;
using NetX.Module;
using NetX.SharedFramework;
using NetX.SharedFramework.ChainPipeline;
using NetX.SharedFramework.ChainPipeline.ChainDataflow;
using System;

namespace Module1;

public class ModuleInitializer1 : ModuleInitializer
{
    public ModuleInitializer1()
    {
    }

    public override Guid Key => new Guid("00000000000000000000000000000002");
    public override ModuleType ModuleType => ModuleType.UserModule;

    public override void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context)
    {
        
    }

    public override void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context)
    {
        //services.AddScoped<ITest, MyTest>();
        services.AddScoped<IEventSubscriber, EventHandler>();
        services.AddScoped<IEventSubscriber, EventHandler1>();

        services.AddChain<DataflowParameterA, DataflowResultA>( ServiceLifetime.Scoped,typeof(ChainMiddlewareA), typeof(ChainMiddlewareB), typeof(ChainMiddlewareC));

        //  services.AddChain<DataflowParameterA, DataflowResultA>(typeof(ActivatorMiddlewareCreater)
        //, typeof(ChainMiddlewareA), typeof(ChainMiddlewareB), typeof(ChainMiddlewareC));
    }
}