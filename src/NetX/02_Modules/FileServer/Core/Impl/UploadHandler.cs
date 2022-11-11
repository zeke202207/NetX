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
    /// 大文件验证
    /// </summary>
    /// <param name="contentType">请求内容类型: 必须 multipart/ 开头</param>
    /// <param name="httpRequestBody">请求body</param>
    /// <returns></returns>
    public ValidateResult Validate(string contentType, Stream httpRequestBody)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// 上传单文件
    /// </summary>
    /// <param name="file">上传文件</param>
    /// <returns></returns>
    public async Task<UploadResult> Upload(IFormFile file)
    {
        using (var stream = file.OpenReadStream())
        {
            Memory<byte> buffer = new Memory<byte>(new byte[file.Length]);
            await stream.ReadAsync(buffer);
            return await IFormFileUpload(buffer, file.FileName);
        }
    }

    /// <summary>
    /// 上传多文件
    /// </summary>
    /// <param name="files">上传文件集合</param>
    /// <returns></returns>
    public async Task<IEnumerable<UploadResult>> Upload(IFormFileCollection files)
    {
        List<UploadResult> result = new List<UploadResult>();
        foreach (var item in files)
            result.Add(await Upload(item));
        return result;
    }

    /// <summary>
    /// 上传大文件
    /// </summary>
    /// <param name="contentType">请求内容类型: 必须 multipart/ 开头</param>
    /// <param name="httpRequestBody">请求body</param>
    /// <returns></returns>
    public Task<UploadResult> Upload(string contentType, Stream httpRequestBody)
    {
        throw new NotImplementedException();
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
    /// <param name="fileName">原始文件名</param>
    /// <returns></returns>
    private async Task<UploadResult> IFormFileUpload(Memory<byte> buffer, string fileName)
    {
        return await _fileService.WriteToFile(fileName, buffer);
    }
}
