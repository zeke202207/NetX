using AutoMapper;
using Dapper;
using Netx.Ddd.Domain;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Models;
using System.Collections.Generic;

namespace NetX.RBAC.Domain;

[Scoped]
public class ApiPagerListQueryHandler : DomainQueryHandler<ApiPagerListQuery, ResultModel>
{
    private readonly IMapper _mapper;

    public ApiPagerListQueryHandler(
        IDatabaseContext dbContext,
        IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public override async Task<ResultModel> Handle(ApiPagerListQuery request, CancellationToken cancellationToken)
    {
        var result = await GetList(request);
        var total = await GetCount(request);
        return result.ToSuccessPagerResultModel(total);
    }

    private async Task<IEnumerable<ApiModel>> GetList(ApiPagerListQuery request)
    {
        string sql = "SELECT * FROM sys_api WHERE 1=@a";
        var param = new DynamicParameters();
        param.Add("a", 1);
        if (!string.IsNullOrWhiteSpace(request.Group))
        {
            sql += " AND group =@group";
            param.Add("group", request.Group);
        }
        sql += $" limit {request.CurrentPage - 1},{request.PageSize}";
        var result = await _dbContext.QueryListAsync<sys_api>(sql, param);
        return _mapper.Map<List<ApiModel>>(result);
    }

    private async Task<int> GetCount(ApiPagerListQuery request)
    {
        string sql = "SELECT COUNT(0) FROM sys_api WHERE 1=@a";
        var param = new DynamicParameters();
        param.Add("a", 1);
        if (!string.IsNullOrWhiteSpace(request.Group))
        {
            sql += " AND group =@group";
            param.Add("group", request.Group);
        }
        return await _dbContext.ExecuteScalarAsync<int>(sql, param);
    }
}

[Scoped]
public class ApiListQueryHandler : DomainQueryHandler<ApiListQuery, ResultModel>
{
    private readonly IMapper _mapper;

    public ApiListQueryHandler(
        IDatabaseContext dbContext,
        IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public override async Task<ResultModel> Handle(ApiListQuery request, CancellationToken cancellationToken)
    {
        var result = await base._dbContext.QueryListAsync<sys_api>("SELECT * FROM sys_api order by group");
        if(result?.Count() ==0)
            return new List<ApiModel>().ToSuccessResultModel();
        return this._mapper.Map<List<ApiModel>>(result).ToSuccessResultModel();
    }
}