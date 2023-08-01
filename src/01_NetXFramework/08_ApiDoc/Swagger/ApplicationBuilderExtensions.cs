using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using System;

namespace NetX.Swagger;

/// <summary>
/// 
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 自定义Swagger
    /// </summary>
    /// <param name="app"></param>
    /// <param name="moduleNames">模块名称</param>
    /// <returns></returns>
    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app, IEnumerable<string> moduleNames)
    {
        app.UseMiniProfiler();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            moduleNames.ToList()
            .ForEach(moduleName =>c.SwaggerEndpoint($"/swagger/{moduleName}/swagger.json", moduleName));
            c.SwaggerEndpoint($"/swagger/{SwaggerConst.C_NOGROUP_NAME}/swagger.json", SwaggerConst.C_NOGROUP_TITLE);
            c.IndexStream = () => typeof(NetX.Swagger.SwaggerConst).GetTypeInfo().Assembly.GetManifestResourceStream("NetX.Swagger.index.html");
        });
        return app;
    }
}
