using AutoMapper;
using Dapper;
using Netx.Ddd.Domain;
using Netx.Ddd.Domain.Extensions;
using NetX.Authentication.Core;
using NetX.Common;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Models;
using NetX.RBAC.Models.Entity.EntityCombination;

namespace NetX.RBAC.Domain.Queries;

[Scoped]
public class LoginQueryHandler : DomainQueryHandler<LoginQuery, ResultModel>
{
    private readonly IEncryption _encryption;
    private readonly ILoginHandler _loginHandler;
    private readonly IMapper _mapper;

    public LoginQueryHandler(IDatabaseContext dbContext,
        IEncryption encryption,
        ILoginHandler loginHandler,
        IMapper mapper)
        : base(dbContext)
    {
        _encryption = encryption;
        _loginHandler = loginHandler;
        _mapper = mapper;
    }

    /// <summary>
    /// 登录处理
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<ResultModel> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        //1. 数据库查询账号密码
        var user = await base._dbContext.QuerySingleAsync<sys_user_roleid_deptid>(@"
SELECT 
    u.*,
    r.roleid as roleid,
    d.deptid as deptid
FROM
    sys_user u
        LEFT JOIN
    sys_user_role r ON r.userid = u.id
        LEFT JOIN
    sys_user_dept d ON d.userid = u.id 
WHERE u.username =@username", new { username = request.UserName });
        if (user == null || !_encryption.Encryption(request.Password).ToUpper().Equals(user.password.ToUpper()))
            return "用户名或密码错误".ToErrorResultModel<ResultModel>();
        if (((Status)user.status) == Status.Disabled)
            return "账号已被禁用".ToErrorResultModel<ResultModel>();
        //2. 生成Token
        string token = await GetToken(new ClaimModel()
        {
            UserId = user.Id,
            LoginName = user.username,
            DisplayName = user.nickname,
            RoleId = user.roleid,
            DeptId = user.deptid
        });
        if (string.IsNullOrWhiteSpace(token))
            return "获取token失败".ToErrorResultModel<ResultModel>();
        return new LoginResult()
        {
            UserId = user.Id,
            UserName = user.username,
            RealName = user.nickname,
            Token = token,
            Desc = user.remark
        }.ToSuccessResultModel();
    }

    /// <summary>
    /// 获取Token信息
    /// </summary>
    /// <param name="model">token声明实体</param>
    /// <returns></returns>
    private async Task<string> GetToken(ClaimModel model)
    {
        var result = this._loginHandler.Handle(model, string.Empty);
        if (null != result)
            return await Task.FromResult(result.AccessToken);
        return await Task.FromResult(string.Empty);
    }
}

[Scoped]
public class LoginUserInfoQueryHandler : DomainQueryHandler<LoginUserInfoQuery, ResultModel>
{
    private readonly IMapper _mapper;

    public LoginUserInfoQueryHandler(IDatabaseContext dbContext,
        IMapper mapper)
        : base(dbContext)
    {
        _mapper = mapper;
    }

    public override async Task<ResultModel> Handle(LoginUserInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await base._dbContext.QuerySingleAsync<sys_user>($"SELECT * FROM sys_user where id =@id and status = 1", new { id = request.UserId });
        if (null == user)
            return $"用户不存在：{request.UserId}".ToErrorResultModel<ResultModel>();
        return this._mapper.Map<UserModel>(user).ToSuccessResultModel();
    }
}

[Scoped]
public class AccountListQueryHandler : DomainQueryHandler<AccountListQuery, ResultModel>
{
    private readonly IMapper _mapper;

    public AccountListQueryHandler(IDatabaseContext dbContext,
        IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public override async Task<ResultModel> Handle(AccountListQuery request, CancellationToken cancellationToken)
    {
        var list = await GetList(request);
        var total = await GetCount(request);
        return list.ToSuccessPagerResultModel<IEnumerable<UserListModel>>(total);
    }

    private async Task<IEnumerable<UserListModel>> GetList(AccountListQuery request)
    {
        string sql = @"SELECT u.* ,r.id as roleid,r.rolename, r.status as rolestatus,d.id as deptid,d.deptname FROM sys_user u
                        left join sys_user_role ur on ur.userid = u.id
                        left join sys_user_dept ud on ud.userid = u.id
                        left join sys_role r on r.id = ur.roleid
                        left join sys_dept d on d.id = ud.deptid
                        where 1=1";
        var param = new DynamicParameters();
        if (!string.IsNullOrWhiteSpace(request.DeptId))
        {
            sql += @" and d.id = @deptid";
            param.Add("deptid", request.DeptId);
        }
        else
            sql += @" and d.id is null or d.id =''";
        if (!string.IsNullOrWhiteSpace(request.NickName))
        {
            sql += @" AND u.nickname LIKE CONCAT('%',@nickname,'%')";
            param.Add("nickname", request.NickName);
        }
        if (!string.IsNullOrWhiteSpace(request.Account))
        {
            sql += @" AND u.username LIKE CONCAT('%',@acoount,'%')";
            param.Add("acoount", request.Account);
        }
        sql += sql.AppendMysqlPagerSql(request.CurrentPage, request.PageSize);
        return await _dbContext.QueryListAsync<UserListModel>(sql, param);
    }

    private async Task<int> GetCount(AccountListQuery request)
    {
        string sql = @"SELECT COUNT(0) FROM sys_user u
                        left join sys_user_role ur on ur.userid = u.id
                        left join sys_user_dept ud on ud.userid = u.id
                        left join sys_role r on r.id = ur.roleid
                        left join sys_dept d on d.id = ud.deptid
                        where 1=1 ";
        var param = new DynamicParameters();
        if (!string.IsNullOrWhiteSpace(request.DeptId))
        {
            sql += @" and d.id = @deptid";
            param.Add("deptid", request.DeptId);
        }
        else
            sql += @" and d.id is null or d.id =''";
        if (!string.IsNullOrWhiteSpace(request.NickName))
        {
            sql += @" AND u.nickname LIKE CONCAT('%',@nickname,'%')";
            param.Add("nickname", request.NickName);
        }
        if (!string.IsNullOrWhiteSpace(request.Account))
        {
            sql += @" AND u.username LIKE CONCAT('%',@acoount,'%')";
            param.Add("acoount", request.Account);
        }
        return await _dbContext.ExecuteScalarAsync<int>(sql, param);
    }
}

[Scoped]
public class AccountPermCodeQueryHandler : DomainQueryHandler<AccountPermCodeQuery, ResultModel>
{
    private readonly IMapper _mapper;

    public AccountPermCodeQueryHandler(IDatabaseContext dbContext,
        IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public override async Task<ResultModel> Handle(AccountPermCodeQuery request, CancellationToken cancellationToken)
    {
        var permissions = await base._dbContext.QueryListAsync<string>(
            @$"SELECT m.permission FROM sys_menu m
                left join sys_role_menu rm on rm.menuid = m.id
                left join sys_user_role ur on ur.roleid = rm.roleid
                where ur.userid = @userid",
            new { userid = request.UserId });
        return permissions.ToSuccessResultModel<IEnumerable<string>>();
    }
}