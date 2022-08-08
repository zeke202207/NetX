namespace NetX
{
    public static class NetXConst
    {
        /// <summary>
        /// server host 模块唯一标识
        /// </summary>
        public static Guid C_SERVERHOST_MODULE_ID = new Guid("00000000000000000000000000000001");

        /// <summary>
        /// 目录名称
        /// </summary>
        public const string C_MODULE_DIRECTORYNAME = "modules";

        /// <summary>
        /// 模块配置文件名称
        /// </summary>
        public const string C_MODULE_CINFIGFILENAME = "plugin.json";

        /// <summary>
        /// Ref引用文件目录
        /// </summary>
        public const string C_MODULE_REFDIRECTORYNAME = "ref";
    }
}
