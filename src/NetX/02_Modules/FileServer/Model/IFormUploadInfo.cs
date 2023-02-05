using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.FileServer.Model
{
    /// <summary>
    /// Form上传实体对象
    /// </summary>
    public class IFormUploadInfo: UploadInfo
    {
        /// <summary>
        /// A file sent with the HttpRequest.
        /// </summary>
        public IFormFile FormFile { get; set; }
    }
}
