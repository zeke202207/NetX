namespace NetX.LocalFileServer.FileStores;

/// <summary>
/// 本地存储配置
/// </summary>
public class LocalStoreConfig
{
    /// <summary>
    /// 存储根目录
    /// </summary>
    public string RootPath { get; set; }

    /// <summary>
    /// 本地存储目录请求path
    /// </summary>
    public string RequestPath { get; set; }

    /// <summary>
    /// 特殊的minitype集合
    /// </summary>
    public Dictionary<string, string> MIMETypes { get; set; } = new();
}
