using Microsoft.AspNetCore.Http;
using NetX.FileServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.FileServer;

/// <summary>
/// 上传处理管理
/// </summary>
public class UploadHandler : IUploader
{
    private readonly IFileStore _fileService;
    private readonly FileServerConfig _serverConfig;

    /// <summary>
    /// 上传处理实例
    /// </summary>
    /// <param name="fileServer">文件服务器</param>
    /// <param name="serverConfig">文件服务器配置项</param>
    public UploadHandler(IFileStore fileServer,FileServerConfig serverConfig)
    {
        this._fileService = fileServer;
        this._serverConfig = serverConfig;
    }

    /// <summary>
    /// 单文件校验
    /// </summary>
    /// <param name="file">上传文件</param>
    /// <returns></returns>
    public ValidateResult Validate(IFormFile file)
    {
        return IFormFileValidate(Path.GetExtension(file.FileName), file.Length);
    }

    /// <summary>
    /// 多文件校验
    /// </summary>
    /// <param name="files">上传文件集合</param>
    /// <returns></returns>
    public ValidateResult Validate(IFormFileCollection files)
    {
        foreach(var file in files)
        {
            var result = Validate(file);
            if(result != ValidateResult.Success)
                return result; 
        }
        return ValidateResult.Success;
    }

    /// <summary>
    /// 上传单文件
    /// </summary>
    /// <param name="uploadInfo">文件类型</param>
    /// <returns></returns>
    public async Task<UploadResult> Upload(IFormUploadInfo uploadInfo)
    {
        using (var stream = uploadInfo.FormFile.OpenReadStream())
        {
            Memory<byte> buffer = new Memory<byte>(new byte[uploadInfo.FormFile.Length]);
            await stream.ReadAsync(buffer);
            return await IFormFileUpload(buffer, uploadInfo);
        }
    }

    /// <summary>
    /// 上传多文件
    /// </summary>
    /// <param name="uploadInfos">文件类型</param>
    /// <returns></returns>
    public async Task<IEnumerable<UploadResult>> Upload(IEnumerable<IFormUploadInfo> uploadInfos)
    {
        List<UploadResult> result = new List<UploadResult>();
        foreach (var item in uploadInfos)
            result.Add(await Upload(item));
        return result;
    }

    /// <summary>
    /// IFormFile 验证
    /// </summary>
    /// <param name="extensionName"></param>
    /// <param name="size"></param>
    /// <returns></returns>
    private ValidateResult IFormFileValidate(string extensionName, long size)
    {
        if(null == _serverConfig)
            return ValidateResult.Success;
        if (!_serverConfig.SupportedExt.Contains(extensionName))
            return ValidateResult.EfficientType;
        if (size >= _serverConfig.LimitedSize)
            return ValidateResult.SizeLimited;
        return ValidateResult.Success;
    }

    /// <summary>
    /// IFormFile方式上传
    /// </summary>
    /// <param name="buffer">文件流</param>
    /// <param name="uploadInfo">原始文件名</param>
    /// <returns></returns>
    private async Task<UploadResult> IFormFileUpload(Memory<byte> buffer, UploadInfo uploadInfo)
    {
        return await _fileService.WriteToFile(uploadInfo, buffer);
    }
}
