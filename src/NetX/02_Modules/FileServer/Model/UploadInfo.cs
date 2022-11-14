using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.FileServer.Model
{
    /// <summary>
    /// 上传文件信息
    /// </summary>
    public class UploadInfo
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { get; set; }

        /// <summary>
        /// 租户标识
        /// </summary>
        public string TenantId { get; set; }

        /// <summary>
        /// 原始文件名
        /// </summary>
        public string OrgFileName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IFormFile FormFile { get; set; }
    }
}
