using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.FileServer.Model;

/// <summary>
/// 上传结果实体对象
/// </summary>
public class UploadResult
{
    /// <summary>
    /// 原始文件名
    /// </summary>
    public string OrgFileName { get; set; }

    /// <summary>
    /// 保存后文件名
    /// </summary>
    public string SaveFileName { get; set; }

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool Success { get; set; }
}
