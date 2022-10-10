using Microsoft.AspNetCore.Mvc;
using NetX.Common.Models;
using NetX.Swagger;
using NetX.Tools.Core;
using NetX.Tools.Models;

namespace NetX.Tools.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiControllerDescription(ToolsConstEnum.C_LOGGING_GROUPNAME, Description = "NetX实现的日志采集模块->日志管理")]
    public class LoggingController : BaseController
    {
        private readonly ILoggingCollectorService _loggingService;

        /// <summary>
        /// 账号管理api实例对象
        /// </summary>
        /// <param name="loggingService"></param>
        public LoggingController(ILoggingCollectorService loggingService)
        {
            this._loggingService = loggingService;
        }

        /// <summary>
        /// 系统登录
        /// </summary>
        /// <returns></returns>
        [ApiActionDescription("获取日志列表")]
        [HttpPost]
        public async Task<ResultModel<PagerResultModel<List<LoggingModel>>>> GetLogList(LoggingParam model)
        {
            return await _loggingService.GetLoggingList(model);
        }

    }
}
