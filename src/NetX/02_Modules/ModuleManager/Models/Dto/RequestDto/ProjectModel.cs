namespace NetX.ModuleManager.Models
{
    /// <summary>
    /// 项目配置
    /// </summary>
    public class ProjectModel
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        /// <example>
        /// NetX.RBAC
        /// </example>
        public string Name { get; set; }

        /// <summary>
        /// 工程别名
        /// </summary>
        /// <example>
        /// RBAC
        /// </example>
        public string Alias { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        /// <example>
        /// 1.0.0.0
        /// </example>
        public string Version { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        /// <example>
        /// true
        /// </example>
        public bool Enabled { get; set; }

        /// <summary>
        ///  是否共享程序集上下文
        /// </summary>
        /// <example>
        /// true
        /// </example>
        public bool IsSharedAssemblyContext { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        /// <example>
        /// at will
        /// </example>
        public string Description { get; set; }
    }
}
