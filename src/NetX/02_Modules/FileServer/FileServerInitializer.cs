using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using NetX.FileServer.FileStores;
using NetX.FileServer.Model;
using NetX.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.FileServer;

/// <summary>
/// 文件服务初始化器
/// </summary>
public class FileServerInitializer : ModuleInitializer
{
    /// <summary>
    /// 模块唯一标识
    /// </summary>
    public override Guid Key => new Guid(FileServerConstEnum.C_FILESERVER_KEY);

    /// <summary>
    /// 模块类型
    /// </summary>
    public override ModuleType ModuleType => ModuleType.UserModule;

    private Func<LocalStoreConfig> UseFileServer;

    private FileExtensionContentTypeProvider ConfigMIMETypeProvider(Dictionary<string, string> minetypes)
    {
        FileExtensionContentTypeProvider provider = new FileExtensionContentTypeProvider();
        foreach (var item in minetypes)
        {
            if (provider.Mappings.ContainsKey(item.Key))
                continue;
            provider.Mappings.Add(item.Key, item.Value);
        }
        return provider;
    }

    /// <summary>
    /// 配置中间件
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    /// <param name="context"></param>
    public override void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, ModuleContext context)
    {
        var config = UseFileServer?.Invoke();
        if(null!= config && !string.IsNullOrEmpty(config.RootPath))
        {
            app.UseFileServer(enableDirectoryBrowsing: true);
            app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            {
                FileProvider = new PhysicalFileProvider(config.RootPath)
            });
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(config.RootPath),
                RequestPath = config.RequestPath,
                ContentTypeProvider = ConfigMIMETypeProvider(config.MIMETypes)
            });
        }
    }

    /// <summary>
    /// 服务注入
    /// </summary>
    /// <param name="services"></param>
    /// <param name="env"></param>
    /// <param name="context"></param>
    public override void ConfigureServices(IServiceCollection services, IWebHostEnvironment env, ModuleContext context)
    {
        services.AddFileServer(() => new FileServerConfig()
        {
            LimitedSize = context.Configuration.GetValue<long>("fileserver:limitedsize") * 1024 * 1024,
            SupportedExt = context.Configuration.GetValue<string>("fileserver:supportedext").Split(';')
        },
        s =>
        {
            var localStoreConfig = new LocalStoreConfig()
            {
                RootPath = context.Configuration.GetValue<string>("localstore:rootpath"),
                RequestPath = context.Configuration.GetValue<string>("localstore:requestpath"),
                MIMETypes = context.Configuration
                .GetSection("localstore:minetype")
                .Get<List<MIMEKeyValueModel>>()
                .ToDictionary(k => k.Key, v => v.Value)
            };
            s.AddScoped<IFileStore, LocalStore>();
            s.AddScoped<LocalStoreConfig>(p => localStoreConfig);
            UseFileServer = () => localStoreConfig;
        });
    }
}
