﻿using Authentication.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetX.Common;
using NetX.DatabaseSetup;
using NetX.Module;
using NetX.RBAC.OAtuhLogin;
using System.Reflection;

namespace NetX.RBAC;

/// <summary>
/// 模块初始化器
/// netx框架自动加载初始化模块
/// </summary>
internal class RBACInitializer : ModuleInitializer
{
    /// <summary>
    /// 模块唯一标识
    /// netx内，不允许重复
    /// </summary>
    public override Guid Key => new Guid(RBACConst.C_RBAC_KEY);

    /// <summary>
    /// 模块类型
    /// 一般指定为用户模块
    /// </summary>
    public override ModuleType ModuleType => ModuleType.UserModule;

    /// <summary>
    /// 配置application
    /// </summary>
    /// <param name="app">A class that provides the mechanisms to configure an application's request pipeline.</param>
    /// <param name="env">Provides information about the web hosting environment an application is running in</param>
    /// <param name="context">模块上下文</param>
    public override void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context)
    {

    }

    /// <summary>
    /// 配置services
    /// </summary>
    /// <param name="services">Specifies the contract for a collection of service descriptors.</param>
    /// <param name="env">Provides information about the web hosting environment an application is running in</param>
    /// <param name="context">模块上下文</param>
    public override void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context)
    {
        //注入mapper
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        //注入加密算法
        services.AddSingleton<IEncryption, MD5>();
        //CodeFirst
        services.AddMigratorAssembly(new Assembly[] { Assembly.GetExecutingAssembly() }, MigrationSupportDbType.MySql5);
        //密码生成策略
        //services.AddScoped<IPasswordStrategy, DefaultPwdStrategy>();
        //添加gitee oauth login
        //services.AddOAuth<DefaultAccessTokenModel, GiteeUserModel>(
        //   new GiteeOAuth(OAuthConfig.Load(context.Configuration, "oauth:gitee")));

        services.AddTransient(typeof(GiteeOAuth), s => new GiteeOAuth(OAuthConfig.Load(context.Configuration, "oauth:gitee")));
        //测试多注入
        services.AddTransient(typeof(GithubOAuth), s => new GithubOAuth(OAuthConfig.Load(context.Configuration, "oauth:gitee")));
    }
}
