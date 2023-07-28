using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace NetX.Swagger;

/// <summary>
/// 
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 添加Swagger
    /// </summary>
    /// <param name="services"></param>
    /// <param name="moduleInfos">模块信息集合</param>
    /// <returns></returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services, IEnumerable<(string name, string version, string des)> moduleInfos)
    {
        //添加miniprofile
        services.AddMiniProfiler(options => {
            options.RouteBasePath = "/profiler";
        }).AddEntityFramework();
        services.AddSwaggerGen(option =>
        {
            moduleInfos.ToList().ForEach(module =>
            {
                option.SwaggerDoc(module.name, ApiInfos(module.name, module.version, module.des));
            });
            //无分组
            option.SwaggerDoc(SwaggerConst.C_NOGROUP_NAME, ApiInfos(SwaggerConst.C_NOGROUP_TITLE, "1.0.0.0", "hello zeke!"));
            //判断接口属于哪个分组
            option.DocInclusionPredicate((docName, apiDes) =>
            {
                if (docName.ToLower().Equals(SwaggerConst.C_NOGROUP_NAME.ToLower()) && string.IsNullOrEmpty(apiDes.GroupName))
                    return true;
                else
                    return apiDes?.GroupName?.ToLower() == docName.ToLower();
            });
            var securityScheme = new OpenApiSecurityScheme
            {
                Description = "JWT认证请求头格式: \"Authorization: Bearer {token}\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = SwaggerConst.C_JWT_SCHEME
            };
            //添加设置Token的按钮
            option.AddSecurityDefinition(SwaggerConst.C_JWT_SCHEME, securityScheme);
            //添加Jwt验证设置
            option.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = SwaggerConst.C_JWT_SCHEME
                        },
                        Scheme = "oauth2",
                        Name = SwaggerConst.C_JWT_SCHEME,
                        In = ParameterLocation.Header,

                    },
                    new List<string>()
                }
            });
            //链接转小写过滤器
            option.DocumentFilter<LowercaseDocumentFilter>();
            //描述信息处理
            option.DocumentFilter<DescriptionDocumentFilter>();
            //隐藏属性
            option.SchemaFilter<IgnorePropertySchemaFilter>();
            //添加自定义header头
            option.OperationFilter<AddRequiredHeaderParameter>();
        });
        return services;
    }

    /// <summary>
    /// 获取api描述信息
    /// </summary>
    /// <param name="title"></param>
    /// <param name="version"></param>
    /// <param name="description"></param>
    /// <returns></returns>
    private static OpenApiInfo ApiInfos(string title, string version, string description) => new OpenApiInfo()
    {
        Title = title,
        Version = version,
        Description = description,
        License = GetLicense(),
        Contact = GetContact(),
        TermsOfService = TermsOfService
    };

    private static OpenApiLicense GetLicense()
    {
        return new OpenApiLicense()
        {
            Name = "MIT License",
            Url = new Uri("https://github.com/zeke202207/netx/blob/dev/LICENSE"),
        };
    }

    private static OpenApiContact GetContact()
    {
        return new OpenApiContact()
        {
            Name = "zeke",
            Email = "mo1986@126.com",
            Url = new Uri("https://github.com/zeke202207/netx")
        };
    }

    private static Uri TermsOfService => new Uri("https://github.com/zeke202207/netx");
}
