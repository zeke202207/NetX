using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.AuditLog
{
    public class HttpContextClientInfoProvider : IClientProvider
    {
        public string BrowserInfo => GetBrowserInfo();
        public string ClientName => GetClientName();
        public string ClientIpAddress => GetClientIpAddress();

        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<HttpContextClientInfoProvider> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="logger"></param>
        public HttpContextClientInfoProvider(IHttpContextAccessor httpContextAccessor, ILogger<HttpContextClientInfoProvider> logger)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        private string GetBrowserInfo()
        {
            return _httpContextAccessor.HttpContext?.Request?.Headers?["User-Agent"];
        }


        private string GetClientName()
        {
            //Todo
            return string.Empty;
        }


        private string GetClientIpAddress()
        {
            try
            {
                return _httpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取客户端ip地址失败");
            }
            return string.Empty;
        }
    }
}
