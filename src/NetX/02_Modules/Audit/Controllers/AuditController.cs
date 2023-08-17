using Microsoft.AspNetCore.Mvc;
using NetX.Audit.Domain;
using NetX.Common.ModuleInfrastructure;
using NetX.Ddd.Core;
using NetX.Swagger;

namespace NetX.Audit.Controllers;

/// <summary>
/// 
/// </summary>
[ApiControllerDescription(AuditConstEnum.C_AUDITLOG_GROUPNAME, Description = "NetX实现的审计日志模块->审计日志管理")]
public class AuditController : BaseController
{
    private readonly IQueryBus _auditLogQuery;

    /// <summary>
    /// 
    /// </summary>
    public AuditController(IQueryBus queryBus)
    {
        _auditLogQuery = queryBus;
    }

    /// <summary>
    /// 获取审计日志
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("获取审计日志列表")]
    [HttpPost]
    public async Task<ResultModel> GetAuditLogs(AuditListParam auditListParam)
    {
        return await _auditLogQuery.Send<AuditListQuery, ResultModel>(new AuditListQuery(auditListParam.NickName, auditListParam.CurrentPage, auditListParam.PageSize));
    }
}
