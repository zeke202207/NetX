using NetX.Module;

namespace NetX.ModuleManager
{
    internal class ModuleManagerConstEnum
    {
        /// <summary>
        /// 脚手架唯一标识
        /// </summary>
        [ModuleKey]
        public const string C_MODULEMANAGER_KEY = "10000000000000000000000000000009";

        /// <summary>
        /// swagger分组名称
        /// </summary>
        public const string C_MODULEMANAGER_GROUPNAME = "modulemanager";

        /// <summary>
        /// 生成项目存放目录
        /// </summary>
        public const string C_CLI_SRC = "src";

        /// <summary>
        /// 模块生成临时目录
        /// </summary>
        public const string C_MODULE_TEMP_PATH = "Module";
    }
}