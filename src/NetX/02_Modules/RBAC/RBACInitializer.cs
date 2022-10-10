﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetX.Common;
using NetX.DatabaseSetup;
using NetX.Module;
using System.Reflection;

namespace NetX.RBAC;

internal class RBACInitializer : ModuleInitializer
{
    public override Guid Key => new Guid(RBACConst.C_RBAC_KEY);

    public override ModuleType ModuleType => ModuleType.UserModule;

    public override void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context)
    {

    }

    public override void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context)
    {
        //注入mapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        //注入加密算法
        services.AddSingleton<IEncryption, MD5>();
        //code first
        services.AddMigratorAssembly(new Assembly[] { Assembly.GetExecutingAssembly() });
    }
}