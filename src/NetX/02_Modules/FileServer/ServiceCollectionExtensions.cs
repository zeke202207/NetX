using Microsoft.Extensions.DependencyInjection;
using NetX.FileServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.FileServer;

/// <summary>
/// 注入文件服务器
/// </summary>
internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// 注入文件服务器
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configServer"></param>
    /// <param name="configStore"></param>
    /// <returns></returns>
    public static IServiceCollection AddFileServer(
        this IServiceCollection services , 
        Func<FileServerConfig> configServer,
        Action<IServiceCollection> configStore)
    {
        services.AddScoped<IUploader,UploadHandler>();
        services.AddScoped<FileServerConfig>(p => configServer.Invoke());
        configStore?.Invoke(services);
        return services;
    }
}
