using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace NetX.QuartzScheduling;

/// <summary>
/// 应用程序扩展类
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <param name="AddQuartzJob"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseQuartzScheduling(this IApplicationBuilder app, Action<IQuartzServer?> AddQuartzJob)
    {
        AddQuartzJob?.Invoke(app.ApplicationServices.GetService<IQuartzServer>());
        return app;
    }
}
