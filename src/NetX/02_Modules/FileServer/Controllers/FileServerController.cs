using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common.Models;
using NetX.Logging.Monitors;
using NetX.Swagger;
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
        /// <returns></returns>
        [ApiActionDescription("测试")]
        [NoPermission]
        [SuppressMonitor]
        [HttpPost]
        public async Task<ResultModel<bool>> TestUpload(IFormFile file)
        {
            var validateResult = this._uploader.Validate(file);
            if (validateResult != ValidateResult.Success)
                return new ResultModel<bool>(ResultEnum.ERROR);
            await this._uploader.Upload(file);
            return await Task.FromResult(new ResultModel<bool>(ResultEnum.SUCCESS));
        }        
    }
}
