using AutoMapper;
using Dapper;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.Ddd.Domain;
using NetX.RBAC.Models;

namespace NetX.RBAC.Domain;


[Scoped]
public class MenuPagerListQueryHandler : DomainQueryHandler<MenuPagerListQuery, ResultModel>
{
    private readonly IMapper _mapper;

    public MenuPagerListQueryHandler(
        IDatabaseContext dbContext,
        IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public override async Task<ResultModel> Handle(MenuPagerListQuery request, CancellationToken cancellationToken)
    {
        string sql = @"SELECT * FROM sys_menu where 1 =1";
        var param = new DynamicParameters();
        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            sql += " AND title LIKE CONCAT('%',@title,'%')";
            param.Add("title", request.Title);
        }
        if (!string.IsNullOrWhiteSpace(request.Status))
        {
            sql += " AND status =@status";
            param.Add("status", request.Status);
        }
        var menus = await base._dbContext.QueryListAsync<sys_menu>(sql, param);
        var result = this._mapper.Map<List<MenuModel>>(menus).ToTree(RBACConst.C_ROOT_ID);
        return result.ToSuccessResultModel();
    }

    private async Task<List<DeptModel>> GetList(MenuPagerListQuery request)
    {
        throw new NotImplementedException();
    }

    private async Task<int> GetCount(MenuPagerListQuery request)
    {
        throw new NotImplementedException();
    }
}


[Scoped]
public class MenuCurrentUserQueryHandler : DomainQueryHandler<MenuCurrentUserQuery, ResultModel>
{
    private readonly IMapper _mapper;

    public MenuCurrentUserQueryHandler(
        IDatabaseContext dbContext,
        IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public override async Task<ResultModel> Handle(MenuCurrentUserQuery request, CancellationToken cancellationToken)
    {
        string sql = $@"SELECT m.* FROM sys_menu m
                    left join sys_role_menu rm on rm.menuid = m.id
                    left join sys_user_role ur on ur.roleid = rm.roleid
                    left join sys_role r on r.id = ur.roleid
                    where ur.userid =@userid
                    AND m.type!=2
                    AND m.status = 1
                    AND r.status = 1";
        var param = new DynamicParameters();
        param.Add("userid", request.UserId);
        var menus = await base._dbContext.QueryListAsync<sys_menu>(sql, param);
        var result = this._mapper.Map<List<MenuModel>>(menus).ToTree(RBACConst.C_ROOT_ID);
        return result.ToSuccessResultModel();
    }
}
