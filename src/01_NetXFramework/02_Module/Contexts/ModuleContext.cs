using Microsoft.Extensions.Configuration;

namespace NetX.Module
{
    /// <summary>
    /// 模块上下文 
    /// </summary>
    public sealed class ModuleContext
    {
        /// <summary>
        /// 配置项
        /// </summary>
        public IConfiguration? Configuration { get; set; }

        /// <summary>
        /// 模块配置项 <see cref="ModuleOptions"/>
        /// </summary>
        public ModuleOptions? ModuleOptions { get; set; }

        /// <summary>
        /// 模块初始化器 <see cref="ModuleInitializer"/>
        /// </summary>
        public ModuleInitializer? Initialize { get; set; }
    }
}
