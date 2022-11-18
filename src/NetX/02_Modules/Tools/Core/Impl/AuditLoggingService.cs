using AutoMapper;
using FreeSql;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.Tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.Core;

/// <summary>
/// 
/// </summary>
[Scoped]
public class AuditLoggingService : LoggingBaseService, IAuditLoggingService
{
    private readonly IBaseRepository<sys_audit_logging> _auditloggingRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 日志管理实例对象
    /// </summary>
    /// <param name="auditloggingRepository"></param>
    /// <param name="mapper"></param>
    public AuditLoggingService(
        IBaseRepository<sys_audit_logging> auditloggingRepository,
        IMapper mapper)
    {
        this._auditloggingRepository = auditloggingRepository;
        this._mapper = mapper;
    }

    /// <summary>
    /// 获取审计日志列表
    /// </summary>
    /// <param name="auditloggingParam"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<PagerResultModel<List<AuditLoggingModel>>>> GetAuditLoggingList(AuditLoggingParam auditloggingParam)
    {
        var list = await _auditloggingRepository.Select
            .WhereIf(!string.IsNullOrWhiteSpace(auditloggingParam.UserName), p => p.username.Equals(auditloggingParam.UserName))
            .OrderByDescending(p => p.createtime)
            .Page(auditloggingParam.Page, auditloggingParam.PageSize).ToListAsync();
        var total = await _auditloggingRepository.Select
            .WhereIf(!string.IsNullOrWhiteSpace(auditloggingParam.UserName), p => p.username.Equals(auditloggingParam.UserName))
            .CountAsync();
        var result = new PagerResultModel<List<AuditLoggingModel>>()
        {
            Items = _mapper.Map<List<AuditLoggingModel>>(list),
            Total = (int)total
        };
        return base.Success<PagerResultModel<List<AuditLoggingModel>>>(result);
    }
}
