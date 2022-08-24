using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NetX.App.AppHandler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.App.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// 启用应用启动处理
        /// </summary>
        /// <returns></returns>
        public static IApplicationBuilder UseStartupHandler(this IApplicationBuilder app)
        {
            var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            _ = applicationLifetime.ApplicationStarted.Register(async () =>
            {
                foreach (var handler in app.ApplicationServices.GetServices<IAppStartHandler>())
                {
                    if (null == handler)
                        continue;
                    await handler.Handle();
                }
            });
            return app;
        }

        /// <summary>
        /// 启用应用停止处理
        /// </summary>
        /// <returns></returns>
        public static IApplicationBuilder UseShutdownHandler(this IApplicationBuilder app)
        {
            var applicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();
            applicationLifetime.ApplicationStopping.Register(async () =>
            {
                foreach (var handler in app.ApplicationServices.GetServices<IAppShutdownHandler>())
                {
                    if (null == handler)
                        continue;
                    await handler.Handle();
                }
            });

            return app;
        }
    }
}
