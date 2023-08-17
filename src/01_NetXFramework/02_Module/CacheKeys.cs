using System.ComponentModel;

namespace NetX.Module
{
    public static class CacheKeys
    {
        /// <summary>
        /// controller 与 key id 缓存 
        /// <para>MODULE:CONTROLLER:MODULEID:</para>
        /// </summary>
        [Description("模块唯一标识")]
        public const string MODULE_CONTROLLER_MODULEID = "MODULE:CONTROLLER:MODULEID:";
    }
}
