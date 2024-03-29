﻿using AutoMapper;
using FreeSql;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.Tools.Models;

namespace NetX.Tools.Core;

/// <summary>
/// 
/// </summary>
[Scoped]
public class LoggingCollectorService : LoggingBaseService, ILoggingCollectorService
{
    private readonly IBaseRepository<sys_logging> _loggingRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 日志管理实例对象
    /// </summary>
    /// <param name="loggingRepository"></param>
    /// <param name="mapper"></param>
    public LoggingCollectorService(
        IBaseRepository<sys_logging> loggingRepository,
        IMapper mapper)
    {
        this._loggingRepository = loggingRepository;
        this._mapper = mapper;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="loggingParam"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ResultModel<PagerResultModel<List<LoggingModel>>>> GetLoggingList(LoggingParam loggingParam)
    {
        var list = await _loggingRepository.Select
            .WhereIf(loggingParam.Level > 0,p=>p.level == loggingParam.Level - 1)
            .OrderByDescending(p => p.createtime)
            .Page(loggingParam.Page, loggingParam.PageSize).ToListAsync();
        var total = await _loggingRepository.Select
            .WhereIf(loggingParam.Level > 0, p => p.level == loggingParam.Level -1).CountAsync();
        var result = new PagerResultModel<List<LoggingModel>>()
        {
            Items = _mapper.Map<List<LoggingModel>>(list),
            Total = (int)total
        };
        return base.Success<PagerResultModel<List<LoggingModel>>>(result);
    }
}
