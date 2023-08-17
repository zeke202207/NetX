using System.ComponentModel;

namespace NetX.DatabaseSetup
{
    /// <summary>
    /// 缓存key
    /// </summary>
    public static class CacheKeys
    {
        /// <summary>
        /// tenant数据库初始化标识缓存key
        /// </summary>
        [Description("租户数据库标识")]
        public const string DATABASESETUP_TENANT_ID = "DATABASESETUP:TENANT:ID:";
    }
}
