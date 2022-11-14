using Microsoft.AspNetCore.Http;
using NetX.FileServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.FileServer;

/// <summary>
/// 上传接口服务
/// </summary>
public interface IUploader
{
    /// <summary>
    /// 单文件校验
    /// </summary>
    /// <param name="file">上传文件</param>
    /// <returns></returns>
    ValidateResult Validate(IFormFile file);

    /// <summary>
    /// 多文件校验
    /// </summary>
    /// <param name="files">上传文件集合</param>
    /// <returns></returns>
    ValidateResult Validate(IFormFileCollection files);

    /// <summary>
    /// 大文件验证
    /// </summary>
    /// <param name="contentType">请求内容类型: 必须 multipart/ 开头</param>
    /// <param name="httpRequestBody">请求body</param>
    /// <returns></returns>
    ValidateResult Validate(string contentType, Stream httpRequestBody);

    /// <summary>
    /// 上传单文件
    /// </summary>
    /// <param name="uploadInfo">文件类型</param>
    /// <returns></returns>
    Task<UploadResult> Upload(UploadInfo uploadInfo);

    /// <summary>
    /// 上传多文件
    /// </summary>
    /// <param name="uploadInfos">文件类型</param>
    /// <returns></returns>
    Task<IEnumerable<UploadResult>> Upload(IEnumerable<UploadInfo> uploadInfos);

    /// <summary>
    /// 上传大文件
    /// </summary>
    /// <param name="contentType">请求内容类型: 必须 multipart/ 开头</param>
    /// <param name="httpRequestBody">请求body</param>
    /// <param name="uploadInfo">文件类型</param>
    /// <returns></returns>
    Task<UploadResult> Upload(string contentType, Stream httpRequestBody, UploadInfo uploadInfo);
}
