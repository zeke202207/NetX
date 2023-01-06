using AutoMapper;
using Netx.Ddd.Domain;
using NetX.Authentication.Core;
using NetX.Common;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public override async Task<ResultModel> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        //1. 数据库查询账号密码
        var user = await base._dbContext.QuerySingleAsync<sys_user>($"SELECT * FROM sys_user WHERE username =@username", new { username = request.UserName });
        if (user == null || !_encryption.Encryption(request.Password).ToUpper().Equals(user.password.ToUpper()))
            return "用户名或密码错误".ToErrorResultModel<ResultModel>();
        //2. 生成Token
        var userInfo = this._mapper.Map<UserModel>(user);
        var userRoleId = await GetRoleId(userInfo.Id);
        //2. 获取token
        string token = await GetToken(new ClaimModel()
        {
            UserId = userInfo.Id,
            LoginName = userInfo.UserName,
            DisplayName = userInfo.NickName,
            RoleId = userRoleId
        });
        if (string.IsNullOrWhiteSpace(token))
            return "获取token失败".ToErrorResultModel<ResultModel>();
        return new LoginResult()
        {
            UserId = userInfo.Id,
            UserName = userInfo.UserName,
            RealName = userInfo.NickName,
            Token = token,
            Desc = userInfo.Remark
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

    /// <summary>
    /// 根据用户Id获取角色id
    /// </summary>
    /// <param name="userId">用户id</param>
    /// <returns></returns>
    private async Task<string> GetRoleId(string userId)
    {
        var userRole = await base._dbContext.QuerySingleAsync<sys_user_role>($"SELECT * FROM sys_user_role where userid=@userid;", new { userid = userId });
        return userRole?.roleid;
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
        _mapper= mapper;
    }

    public override async Task<ResultModel> Handle(LoginUserInfoQuery request, CancellationToken cancellationToken)
    {
        var user = await base._dbContext.QuerySingleAsync<sys_user>($"SELECT * FROM sys_user where id =@id and status = 1", new { id = request.UserId });
        if(null == user)
            return $"用户不存在：{request.UserId}".ToErrorResultModel<ResultModel>();
        return this._mapper.Map<UserModel>(user).ToSuccessResultModel();
    }
}