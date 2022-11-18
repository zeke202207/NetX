using AutoMapper;
using FreeSql;
using NetX.Common;
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
/// 登录日志服务
/// </summary>
[Scoped]
public class LoginLoggingService : BaseService, ILoginLoggingService
{
    private readonly IBaseRepository<sys_login_logging> _loginLoggingRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="loginLoggingRepository"></param>
    /// <param name="mapper"></param>
    public LoginLoggingService(
        IBaseRepository<sys_login_logging> loginLoggingRepository,
        IMapper mapper)
    {
        this._loginLoggingRepository = loginLoggingRepository;
        this._mapper = mapper;
    }

    /// <summary>
    /// 获取登录日志列表
    /// </summary>
    /// <param name="queryParam">查询参数</param>
    /// <returns></returns>
    public async Task<ResultModel<PagerResultModel<List<LoginLoggingModel>>>> GetLogininLoggingList(LoginLoggingParam queryParam)
    {
        var list = await _loginLoggingRepository.Select
            .WhereIf(!string.IsNullOrWhiteSpace(queryParam.UserName),p=>p.username.Contains(queryParam.UserName))
           .OrderByDescending(p => p.createtime)
           .Page(queryParam.Page, queryParam.PageSize).ToListAsync();
        var total = await _loginLoggingRepository.Select.CountAsync();
        var result = new PagerResultModel<List<LoginLoggingModel>>()
        {
            Items = _mapper.Map<List<LoginLoggingModel>>(list),
            Total = (int)total
        };
        return base.Success<PagerResultModel<List<LoginLoggingModel>>>(result);
    }
}
