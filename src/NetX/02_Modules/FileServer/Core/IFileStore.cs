using NetX.FileServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.FileServer;

/// <summary>
/// 文件服务器
/// </summary>
public interface IFileStore
{
    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="fileName">上传文件名</param>
    /// <param name="buffer">文件数组</param>
    /// <returns></returns>
    Task<UploadResult> WriteToFile(string fileName ,byte[] buffer);

    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="fileName">上传文件名</param>
    /// <param name="buffer">文件数组</param>
    /// <returns></returns>
    Task<UploadResult> WriteToFile(string fileName, Memory<byte> buffer);

    /// <summary>
    /// 文件是否存在
    /// </summary>
    /// <param name="fileName">上文件名</param>
    /// <returns></returns>
    Task<bool> IsExist(string fileName);

    /// <summary>
    /// 获取文件数据
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    Task<byte[]> ReadFromFile(string fileName);

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    Task<bool> RemoveFile(string fileName);
}
