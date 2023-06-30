using Microsoft.AspNetCore.Http;

namespace NetX.SimpleFileSystem.Model
{
    /// <summary>
    /// Form上传实体对象
    /// </summary>
    public class IFormUploadInfo : UploadInfo
    {
        /// <summary>
        /// A file sent with the HttpRequest.
        /// </summary>
        public IFormFile FormFile { get; set; }
    }
}
