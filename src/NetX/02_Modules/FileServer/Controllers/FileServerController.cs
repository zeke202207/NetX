using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common;
using NetX.Common.ModuleInfrastructure;
using NetX.FileServer.Model;
using NetX.Swagger;
using NetX.Tenants;

namespace NetX.FileServer.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiControllerDescription(FileServerConstEnum.C_FILESERVER_GROUPNAME, Description = "NetX实现的文件系统模块")]
    public class FileServerController : BaseController
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
        /// 上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="slug">文件类型</param>
        /// <returns></returns>
        [ApiActionDescription("上传文件")]
        [NoPermission]
        [HttpPost("/netx/upload/{slug}")]
        public async Task<ResultModel> Upload(IFormFile file, int slug)
        {
            var validateResult = this._uploader.Validate(file);
            if (validateResult != ValidateResult.Success)
                return new ResultModel<UploadResult>(ResultEnum.ERROR);
            var uploadResult = await this._uploader.Upload(new IFormUploadInfo()
            {
                TenantId = TenantContext.CurrentTenant.Principal?.Tenant.TenantId ?? String.Empty,
                FileType = (FileType)slug,
                OrgFileName = file.FileName,
                FormFile = file
            });
            return await Task.FromResult(new ResultModel<UploadResult>(ResultEnum.SUCCESS) { Result = uploadResult });
        }

        /// <summary>
        /// 批量上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <param name="slug">文件类型</param>
        /// <returns></returns>
        [ApiActionDescription("批量上传文件")]
        [NoPermission]
        [HttpPost("/netx/uploadbatch/{slug}")]
        public async Task<ResultModel> UploadBatch(IFormFileCollection file, int slug)
        {
            var validateResult = this._uploader.Validate(file);
            if (validateResult != ValidateResult.Success)
                return new ResultModel<List<UploadResult>>(ResultEnum.ERROR);
            var uploadInfos = file.Select(p => new IFormUploadInfo()
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
