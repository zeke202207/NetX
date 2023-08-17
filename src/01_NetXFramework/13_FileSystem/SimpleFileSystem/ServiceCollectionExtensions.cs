using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetX.Module;
using NetX.SimpleFileSystem.Model;

namespace NetX.SimpleFileSystem;

/// <summary>
/// 注入文件服务器
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 注入文件系统
    /// </summary>
    /// <param name="services"></param>
    /// <param name="context"></param>
    /// <param name="configStore"></param>
    /// <returns></returns>
    public static IServiceCollection AddSimpleFileSystem(
        this IServiceCollection services,
        ModuleContext context,
        Action<IServiceCollection> configStore)
    {
        //上传最大限制 单位 M
        //最大值 20M 更大文件建议使用断点续传方式
        var maxLimitedSize = context.Configuration.GetValue<long>("fileserver:limitedsize") * 1024 * 1024;
        if (maxLimitedSize > 20 * 1024 * 1024)
            maxLimitedSize = 20 * 1024 * 1024;
        var config = new FileServerConfig()
        {
            LimitedSize = maxLimitedSize,
            SupportedExt = context.Configuration.GetValue<string>("fileserver:supportedext").Split(';'),
        };

        //设置服务上传文件大小限制
        services.Configure<FormOptions>(x =>
        {
            x.MultipartBodyLengthLimit = maxLimitedSize;
            x.ValueLengthLimit = int.MaxValue;
        });
        services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = maxLimitedSize;
        });

        services.AddScoped<IUploader, UploadHandler>();
        services.AddSingleton<FileServerConfig>(p => config);
        configStore?.Invoke(services);
        return services;
    }
}
