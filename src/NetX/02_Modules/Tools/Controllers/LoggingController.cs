using Microsoft.AspNetCore.Mvc;
using NetX.Common.ModuleInfrastructure;
using NetX.Swagger;
using NetX.Tools.Core;
using NetX.Tools.Models;

namespace NetX.Tools.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiControllerDescription(LoggingConstEnum.C_LOGGING_GROUPNAME, Description = "NetX实现的日志采集模块->日志管理")]
    public class LoggingController : BaseController
    {
        private readonly ILoggingCollectorService _loggingService;
        private readonly IAuditLoggingService _auditLoggingService;
        private readonly ILoginLoggingService _loginLoggingService;

        /// <summary>
        /// 账号管理api实例对象
        /// </summary>
        /// <param name="loggingService">系统日志服务</param>
        /// <param name="auditLoggingService">审核日志服务</param>
        /// <param name="loginLoggingService">登录日志服务</param>
        public LoggingController(ILoggingCollectorService loggingService, 
            IAuditLoggingService auditLoggingService,
            ILoginLoggingService loginLoggingService)
        {
            this._loggingService = loggingService;
            this._auditLoggingService = auditLoggingService;
            this._loginLoggingService = loginLoggingService;
        }

        /// <summary>
        /// 获取系统日志列表
        /// </summary>
        /// <param name="queryParam">查询参数</param>
        /// <returns></returns>
        [ApiActionDescription("获取系统日志列表")]
        [HttpPost]
        public async Task<ResultModel> GetLogList(LoggingParam queryParam)
        {
            return await _loggingService.GetLoggingList(queryParam);
        }

        /// <summary>
        /// 获取审计日志列表
        /// 审计日志，需要在action上添加 [Audit] 特性
        /// </summary>
        /// <param name="queryParam">查询参数</param>
        /// <returns></returns>
        [ApiActionDescription("获取审计日志列表")]
        [HttpPost]
        public async Task<ResultModel> GetAuditLogList(AuditLoggingParam queryParam)
        {
            return await _auditLoggingService.GetAuditLoggingList(queryParam);
        }

        /// <summary>
        /// 获取登录日志列表
        /// </summary>
        /// <param name="queryParam">查询参数</param>
        /// <returns></returns>
        [ApiActionDescription("获取登录日志列表")]
        [HttpPost]
        public async Task<ResultModel> GetLoginLogList(LoginLoggingParam queryParam)
        {
            return await _loginLoggingService.GetLogininLoggingList(queryParam);
        }
    }
}
