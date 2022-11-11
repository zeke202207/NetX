using Microsoft.Extensions.Logging;
using NetX.FileServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.FileServer.FileStores;

/// <summary>
/// 本地存储文件服务
/// </summary>
public class LocalStore : IFileStore
{
    private readonly LocalStoreConfig _storeConfig;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="storeConfig"></param>
    public LocalStore(LocalStoreConfig storeConfig)
    {
        _storeConfig = storeConfig;
    }

    /// <summary>
    /// 文件是否存在
    /// </summary>
    /// <param name="fileName">上文件名</param>
    /// <returns></returns>
    public async Task<bool> IsExist(string fileName)
    {
        var fullPath = Path.Combine(_storeConfig.RootPath, fileName);
        return await Task.FromResult(File.Exists(fullPath));
    }

    /// <summary>
    /// 获取文件数据
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    public async Task<byte[]> ReadFromFile(string fileName)
    {
        var fullPath = Path.Combine(_storeConfig.RootPath, fileName);
        if (!File.Exists(fullPath))
            return await Task.FromResult(new byte[0]);
        return await File.ReadAllBytesAsync(fullPath);
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <returns></returns>
    public async Task<bool> RemoveFile(string fileName)
    {
        var fullPath = Path.Combine(_storeConfig.RootPath, fileName);
        if (!File.Exists(fullPath))
            return true;
        File.Delete(fullPath);
        return await Task.FromResult(true);
    }

    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="fileName">上传文件名</param>
    /// <param name="buffer">文件数组</param>
    /// <returns></returns>
    public async Task<UploadResult> WriteToFile(string fileName, byte[] buffer)
    {
        return await Write(fileName, buffer);
    }

    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="fileName">上传文件名</param>
    /// <param name="buffer">文件数组</param>
    /// <returns></returns>
    public async Task<UploadResult> WriteToFile(string fileName, Memory<byte> buffer)
    {
        return await Write(fileName, buffer.ToArray());
    }

    /// <summary>
    /// 保存文件到本地
    /// </summary>
    /// <param name="fileName">文件名</param>
    /// <param name="buffer">文件数组</param>
    /// <returns></returns>
    private async Task<UploadResult> Write(string fileName, byte[] buffer)
    {
        var filePath = GetFileName(fileName);
        var fullPath = Path.Combine(_storeConfig.RootPath, filePath);
        var dir = Path.GetDirectoryName(fullPath);
        if (!Directory.Exists(dir))
            Directory.CreateDirectory(dir);
        await File.WriteAllBytesAsync(fullPath, buffer.ToArray());
        return new UploadResult()
        {
            Success = true,
            OrgFileName = filePath,
            SaveFileName = filePath.Replace("\\", "/")
        };
    }

    /// <summary>
    /// 生成保存文件名
    /// </summary>
    /// <returns></returns>
    private string GetFileName(string fileName)
    {
        var ext = Path.GetExtension(fileName);
        var date = DateTime.Now;
        return Path.Combine(date.ToString("yyyy"), date.ToString("MM"), Guid.NewGuid().ToString("N")) + ext;
    }
}
