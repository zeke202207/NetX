using NetX.SimpleFileSystem;
using NetX.SimpleFileSystem.Model;

namespace NetX.LocalFileServer.FileStores;

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
    /// <param name="uploadInfo">上传文件名</param>
    /// <param name="buffer">文件数组</param>
    /// <returns></returns>
    public async Task<UploadResult> WriteToFile(UploadInfo uploadInfo, byte[] buffer)
    {
        return await Write(uploadInfo, buffer);
    }

    /// <summary>
    /// 保存文件
    /// </summary>
    /// <param name="uploadInfo">上传文件名</param>
    /// <param name="buffer">文件数组</param>
    /// <returns></returns>
    public async Task<UploadResult> WriteToFile(UploadInfo uploadInfo, Memory<byte> buffer)
    {
        return await Write(uploadInfo, buffer.ToArray());
    }

    /// <summary>
    /// 保存文件到本地
    /// </summary>
    /// <param name="uploadInfo">文件名</param>
    /// <param name="buffer">文件数组</param>
    /// <returns></returns>
    private async Task<UploadResult> Write(UploadInfo uploadInfo, byte[] buffer)
    {
        var filePath = GetFileName(uploadInfo);
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
    private string GetFileName(UploadInfo uploadInfo)
    {
        var tenantId = uploadInfo.TenantId;
        if (string.IsNullOrWhiteSpace(tenantId))
            tenantId = "netx";
        var ext = Path.GetExtension(uploadInfo.OrgFileName);
        var date = DateTime.Now;
        return Path.Combine(
            uploadInfo.TenantId.ToLower(),
            uploadInfo.FileType.ToString().ToLower(),
            date.ToString("yyyy"),
            date.ToString("MM"),
            Guid.NewGuid().ToString("N").ToLower())
            + ext;
    }
}
