namespace NetX.AuditLog
{
    public interface IClientProvider
    {
        /// <summary>
        /// 浏览器信息
        /// </summary>
        string BrowserInfo { get; }

        /// <summary>
        /// 客户端信息
        /// </summary>
        string ClientName { get; }

        /// <summary>
        /// 客户端IP地址
        /// </summary>
        string ClientIpAddress { get; }
    }
}
