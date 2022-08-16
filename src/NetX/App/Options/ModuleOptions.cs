namespace NetX
{
    /// <summary>
    /// 模块配置项
    /// </summary>
    public sealed class ModuleOptions
    {
        /// <summary>
        /// Module唯一标识
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 插件模块入口dll名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 依赖文件列表
        /// 相对地址
        /// </summary>
        public List<string> Dependencies { get; set; } = new();
    }
}
