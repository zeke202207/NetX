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
        private readonly IAuditLoggingService _auditLoggingService;

        /// <summary>
        /// 账号管理api实例对象
        /// </summary>
        /// <param name="loggingService">系统日志服务</param>
        /// <param name="auditLoggingService">审核日志服务</param>
        public LoggingController(ILoggingCollectorService loggingService, 
            IAuditLoggingService auditLoggingService)
        {
            this._loggingService = loggingService;
            this._auditLoggingService = auditLoggingService;
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

        /// <summary>
        /// 系统登录
        /// </summary>
        /// <returns></returns>
        [ApiActionDescription("获取审计日志列表")]
        [HttpPost]
        public async Task<ResultModel<PagerResultModel<List<AuditLoggingModel>>>> GetAuditLogList(AuditLoggingParam model)
        {
            return await _auditLoggingService.GetAuditLoggingList(model);
        }

    }
}
