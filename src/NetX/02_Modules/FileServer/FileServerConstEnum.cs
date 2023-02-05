using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.FileServer;

internal class FileServerConstEnum
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    public const string C_FILESERVER_KEY = "10000000000000000000000000000003";

    /// <summary>
    /// swagger分组名称
    /// </summary>
    public const string C_FILESERVER_GROUPNAME = "fileserver";
}

/// <summary>
/// 验证结果枚举
/// </summary>
public enum ValidateResult
{
    /// <summary>
    /// 验证成功
    /// </summary>
    Success,

    /// <summary>
    /// 文件大小受限制
    /// </summary>
    SizeLimited,

    /// <summary>
    /// 文件类型无效
    /// </summary>
    EfficientType,

    /// <summary>
    /// 不支持的上传类型
    /// </summary>
    UnSupportType,
}

/// <summary>
/// 文件类型
/// </summary>
public enum FileType : int
{
    /// <summary>
    /// 图片
    /// </summary>
    Image = 0,

    /// <summary>
    /// 音频
    /// </summary>
    Audio,

    /// <summary>
    /// 视频
    /// </summary>
    Video,

    /// <summary>
    /// 其他
    /// </summary>
    Others,
}
