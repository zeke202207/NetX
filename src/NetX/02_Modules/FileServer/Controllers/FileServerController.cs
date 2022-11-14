﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common.Models;
using NetX.FileServer.Model;
using NetX.Logging.Monitors;
using NetX.Swagger;
using NetX.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.FileServer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiControllerDescription(FileServerConstEnum.C_FILESERVER_GROUPNAME, Description = "NetX实现的文件系统模块")]
    public class FileServerController: BaseController
    {
        private readonly IUploader _uploader;

        /// <summary>
        /// 实例对象
        /// </summary>
        /// <param name="uploader"></param>
        public FileServerController(IUploader uploader)
        {
            this._uploader = uploader;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="slug">文件类型</param>
        /// <returns></returns>
        [ApiActionDescription("上传文件")]
        [NoPermission]
        [SuppressMonitor]
        [HttpPost("/netx/upload/{slug}")]
        public async Task<ResultModel<UploadResult>> Upload(IFormFile file, int slug)
        {
            var validateResult = this._uploader.Validate(file);
            if (validateResult != ValidateResult.Success)
                return new ResultModel<UploadResult>(ResultEnum.ERROR);
            var uploadResult = await this._uploader.Upload(new UploadInfo()
            {
                TenantId = TenantContext.CurrentTenant.Principal?.Tenant.TenantId ?? String.Empty,
                FileType = (FileType)slug,
                OrgFileName = file.FileName,
                FormFile = file
            });
            return await Task.FromResult(new ResultModel<UploadResult>(ResultEnum.SUCCESS) { Result = uploadResult });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="slug">文件类型</param>
        /// <returns></returns>
        [ApiActionDescription("批量上传文件")]
        [NoPermission]
        [SuppressMonitor]
        [HttpPost("/netx/uploadbatch/{slug}")]
        public async Task<ResultModel<List<UploadResult>>> UploadBatch(IFormFileCollection file, int slug)
        {
            var validateResult = this._uploader.Validate(file);
            if (validateResult != ValidateResult.Success)
                return new ResultModel<List<UploadResult>>(ResultEnum.ERROR);
            var uploadInfos = file.Select(p => new UploadInfo()
            {
                TenantId = TenantContext.CurrentTenant.Principal?.Tenant.TenantId ?? String.Empty,
                FileType = (FileType)slug,
                OrgFileName = p.FileName,
                FormFile = p
            });
            var uploadResult = await this._uploader.Upload(uploadInfos);
            return await Task.FromResult(new ResultModel<List<UploadResult>>(ResultEnum.SUCCESS) { Result = uploadResult?.ToList() });
        }
    }
}
