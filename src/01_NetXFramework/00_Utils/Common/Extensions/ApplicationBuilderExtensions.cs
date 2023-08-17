using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace NetX.Common;

/// <summary>
/// 应用程序扩展方法
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IApplicationBuilder UseAppHandler(this IApplicationBuilder app)
    {
        var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
        RegisterStarted(app, applicationLifetime);
        RegisterShutdown(app, applicationLifetime);
        return app;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <param name="applicationLifetime"></param>
    private static void RegisterStarted(IApplicationBuilder app, IHostApplicationLifetime applicationLifetime)
    {
        applicationLifetime.ApplicationStarted.Register(async () =>
        {
            foreach (var handler in app.ApplicationServices.GetServices<IStartHandler>())
            {
                if (null == handler)
                    continue;
                await handler.Handle();
            }
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <param name="applicationLifetime"></param>
    private static void RegisterShutdown(IApplicationBuilder app, IHostApplicationLifetime applicationLifetime)
    {
        applicationLifetime.ApplicationStopping.Register(async () =>
        {
            foreach (var handler in app.ApplicationServices.GetServices<IShutdownHandler>())
            {
                if (null == handler)
                    continue;
                await handler.Handle();
            }
        });
    }
}
