using NetX.Common.ModuleInfrastructure;
using NetX.Tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.Core;

/// <summary>
/// 审计日志服务
/// </summary>
public interface IAuditLoggingService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="auditloggingParam"></param>
    /// <returns></returns>
    Task<ResultModel<PagerResultModel<List<AuditLoggingModel>>>> GetAuditLoggingList(AuditLoggingParam auditloggingParam);
}
