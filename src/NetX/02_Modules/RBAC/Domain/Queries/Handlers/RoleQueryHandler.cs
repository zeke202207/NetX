using AutoMapper;
using Dapper;
using Netx.Ddd.Domain;
using Netx.Ddd.Domain.Extensions;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Models;

namespace NetX.RBAC.Domain;

[Scoped]
public class RolePagerListQueryHandler : DomainQueryHandler<RolePagerListQuery, ResultModel>
{
    private readonly IMapper _mapper;

    public RolePagerListQueryHandler(
        IDatabaseContext dbContext,
        IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public override async Task<ResultModel> Handle(RolePagerListQuery request, CancellationToken cancellationToken)
    {
        var result = await GetList(request);
        return result.ToSuccessResultModel();
        //var count = await GetCount(request);
        //return result.ToSuccessPagerResultModel(count);
    }

    private async Task<List<RoleModel>> GetList(RolePagerListQuery request)
    {
        string sql = @"SELECT * FROM sys_role where 1 =1";
        var param = new DynamicParameters();
        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            sql += " AND status =@status";
            param.Add("status", request.Status);
        }
        if (!string.IsNullOrWhiteSpace(request.RoleName))
        {
            sql += " AND rolename LIKE CONCAT('%',@rolename,'%')";
            param.Add("rolename", request.RoleName);
        }
        sql += sql.AppendMysqlPagerSql(request.CurrentPage, request.PageSize);
        var roleEntities = await base._dbContext.QueryListAsync<sys_role>(sql, param);
        List<(sys_role role, List<string> menuids)> result = new List<(sys_role role, List<string> menuids)>();
        foreach (var roleEntity in roleEntities)
        {
            var sqlMenuIds = $@"SELECT  m.id FROM sys_role_menu rm
                                    LEFT JOIN sys_menu m ON m.id = rm.menuid
                             WHERE
                                rm.roleid = @roleid";
            var menuIds = await base._dbContext.QueryListAsync<string>(sqlMenuIds, new { roleid = roleEntity.Id });

            result.Add((role: roleEntity, menuids: menuIds.ToList()));
        }
        return this._mapper.Map<List<RoleModel>>(result);
    }

    private async Task<int> GetCount(RolePagerListQuery request)
    {
        string sql = @"SELECT count(0) FROM sys_role where 1 =1";
        var param = new DynamicParameters();
        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            sql += " AND status =@status";
            param.Add("status", request.Status);
        }
        if (!string.IsNullOrWhiteSpace(request.RoleName))
        {
            sql += " AND rolename LIKE CONCAT('%',@rolename,'%')";
            param.Add("rolename", request.RoleName);
        }
        return await _dbContext.ExecuteScalarAsync<int>(sql, param);
    }
}

[Scoped]
public class RoleApiQueryHandler : DomainQueryHandler<RoleApiQuery, ResultModel>
{
    private readonly IMapper _mapper;

    public RoleApiQueryHandler(
        IDatabaseContext dbContext,
        IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public override async Task<ResultModel> Handle(RoleApiQuery request, CancellationToken cancellationToken)
    {
        string sql = @"SELECT apiid FROM sys_role_api where roleid =@roleid";
        var param = new DynamicParameters();
        param.Add("roleid", request.Id);
        var apiids = await base._dbContext.QueryListAsync<string>(sql, param);
        return apiids.ToSuccessResultModel();
    }
}
