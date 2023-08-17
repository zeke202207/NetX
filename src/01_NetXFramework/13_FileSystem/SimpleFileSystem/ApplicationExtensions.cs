using Microsoft.AspNetCore.Builder;
using NetX.Module;

namespace NetX.SimpleFileSystem
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// 使用简单文件系统
        /// </summary>
        /// <param name="app"></param>
        /// <param name="context"></param>
        /// <param name="MINITypes"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSimpleFileSystem(
            this IApplicationBuilder app,
            ModuleContext context,
            Action<IApplicationBuilder> appAction)
        {
            appAction?.Invoke(app);
            return app;
        }
    }
}
