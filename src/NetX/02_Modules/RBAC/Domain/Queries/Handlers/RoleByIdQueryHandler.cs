using AutoMapper;
using Dapper;
using NetX.Ddd.Domain;
using NetX.Ddd.Domain.Extensions;
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
        string sql = @"SELECT 
    r.*,
    t.menuids
FROM
    sys_role r
        LEFT JOIN
    (SELECT 
    m.roleid, GROUP_CONCAT(m.menuid) AS menuids
FROM
    sys_role_menu m
GROUP BY m.roleid) t
on t.roleid = r.id  where 1 =1 ";
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
        var roleEntities = await base._dbContext.QueryListAsync<rolemenuids>(sql, param);
        return roleEntities.ToList().Select(p => new RoleModel()
        {
            Id = p.Id,
            ApiCheck = p.apicheck.ToString(),
            CheckMenu = new CheckMenu() { Checked = p.menuids?.Split(",").ToList(), HalfChecked = new List<string>() },
            CreateTime = p.createtime,
            IsSystem = p.issystem,
            Remark = p.remark,
            RoleName = p.rolename,
            Status = p.status.ToString(),
            Menus = p.menuids?.Split(",").ToList()
        }).ToList();
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

    private class rolemenuids: sys_role
    {
        public string menuids { get; set; }
    }
}


[Scoped]
public class RoleByIdQueryHandler : DomainQueryHandler<RoleByIdQuery, ResultModel>
{
    private readonly IMapper _mapper;

    public RoleByIdQueryHandler(
        IDatabaseContext dbContext,
        IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public override async Task<ResultModel> Handle(RoleByIdQuery request, CancellationToken cancellationToken)
    {
        string sql = @"SELECT * FROM sys_role WHERE id =@roleid";
        var param = new DynamicParameters();
        param.Add("roleid", request.Id);
        var roleModel = await base._dbContext.QuerySingleAsync<RoleModel>(sql, param);
        return roleModel.ToSuccessResultModel();
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
        string sql = @"SELECT 
    a.*
FROM
    sys_role_api ra
        LEFT JOIN
    sys_api a ON a.id = ra.apiid
WHERE
	ra.roleid =@roleid";
        var param = new DynamicParameters();
        param.Add("roleid", request.RoleId);
        var list = await base._dbContext.QueryListAsync<ApiModel>(sql, param);
        return list.ToSuccessResultModel();
    }
}


[Scoped]
public class RoleApiIdsQueryHandler : DomainQueryHandler<RoleApiIdsQuery, ResultModel>
{
    private readonly IMapper _mapper;

    public RoleApiIdsQueryHandler(
        IDatabaseContext dbContext,
        IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public override async Task<ResultModel> Handle(RoleApiIdsQuery request, CancellationToken cancellationToken)
    {
        string sql = @"SELECT apiid FROM sys_role_api where roleid =@roleid";
        var param = new DynamicParameters();
        param.Add("roleid", request.RoleId);
        var apiids = await base._dbContext.QueryListAsync<string>(sql, param);
        return apiids.ToSuccessResultModel();
    }
}
