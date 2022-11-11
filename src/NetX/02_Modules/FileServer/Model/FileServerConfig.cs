using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.FileServer.Model;

/// <summary>
/// 文件服务器配置
/// </summary>
public class FileServerConfig
{
    /// <summary>
    /// 支持的文件扩展名集合
    /// </summary>
    public IEnumerable<string> SupportedExt { get; set; } = Enumerable.Empty<string>();

    /// <summary>
    /// 单文件最大限制
    /// </summary>
    public long? LimitedSize { get; set; }
}
