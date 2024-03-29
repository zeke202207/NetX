﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using NetX.Audit.Provider;
using NetX.AuditLog;
using NetX.DatabaseSetup;
using NetX.Module;
using System.Reflection;

namespace NetX.Audit
{
    public class AuditInitializer : ModuleInitializer
    {
        /// <summary>
        /// 模块唯一标识
        /// netx内，不允许重复
        /// </summary>
        public override Guid Key => new Guid(AuditConstEnum.C_AUDIT_KEY);

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
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            //CodeFirst
            services.AddMigratorAssembly(new Assembly[] { Assembly.GetExecutingAssembly() }, MigrationSupportDbType.MySql5);
            //1. 审计日志
            services.AddAuditLog(o =>
            {
                o.Enabled = true;
            },
            typeof(DatabaseStoreProvider));
        }
    }
}