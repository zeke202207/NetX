using AutoMapper;
using FreeSql;
using NetX.Authentication.Core;
using NetX.Common;
using NetX.Common.Attributes;
using NetX.Common.Models;
using NetX.RBAC.Data.Repositories;
using NetX.RBAC.Models;

namespace NetX.RBAC.Core;

/// <summary>
/// 账号管理服务
/// </summary>
[Scoped]
public class AccountService : RBACBaseService, IAccountService
{
    private readonly IBaseRepository<sys_user> _userRepository;
    private readonly IEncryption _encryption;
    private readonly ILoginHandler _loginHandler;
    private readonly IMapper _mapper;
    private readonly IPasswordStrategy _pwdStrategy;

    /// <summary>
    /// 账号管理服务实例对象
    /// </summary>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="encryption">加密算法</param>
    /// <param name="loginHandler">JWT票据处理实例</param>
    /// <param name="mapper">实体对象映射实例</param>
    /// <param name="strategy">密码生成策略</param>
    public AccountService(
        IBaseRepository<sys_user> userRepository,
        IEncryption encryption,
        ILoginHandler loginHandler,
        IMapper mapper,
        IPasswordStrategy strategy)
    {
        this._userRepository = userRepository;
        this._encryption = encryption;
        this._loginHandler = loginHandler;
        this._mapper = mapper;
        this._pwdStrategy=strategy;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="username">用户名</param>
    /// <param name="password">密码</param>
    /// <returns></returns>
    public async Task<ResultModel<LoginResult>> Login(string username, string password)
    {
        //1. 数据库查询账号
        var user = await this._userRepository.Select.Where(p => p.username.Equals(username)).FirstAsync<sys_user>();
        if (null == user || _encryption.Encryption(password).ToLower().Equals(user.password))
            return base.Error<LoginResult>("用户名或密码错误");
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
            return base.Error<LoginResult>("获取token失败");
        return base.Success<LoginResult>(new LoginResult()
        {
            UserId = userInfo.Id,
            UserName = userInfo.UserName,
            RealName = userInfo.NickName,
            Token = token,
            Desc = userInfo.Remark
        });
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
        return await ((SysUserRepository)_userRepository).GetRoleId(userId);
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="userId">用户唯一标识</param>
    /// <returns></returns>
    public async Task<ResultModel<UserModel>> GetUserInfo(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return base.Error<UserModel>("获取当前登录用户id失败");
        var user = await this._userRepository.Select.Where(p => p.id.Equals(userId) && p.status.Equals((int)Status.Enable)).FirstAsync<sys_user>();
        if (null == user)
            return base.Error<UserModel>("用户不存在");
        return base.Success<UserModel>(this._mapper.Map<UserModel>(user));
    }

    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <param name="queryParam">查询条件实体</param>
    /// <returns></returns>
    public async Task<ResultModel<PagerResultModel<List<UserListModel>>>> GetAccountLists(UserListParam queryParam)
    {
        var listTotal = await ((SysUserRepository)_userRepository)
            .GetUserListAsync(queryParam.DeptId, queryParam.Account, queryParam.Nickname, queryParam.Page, queryParam.PageSize);
        List<UserListModel> tempUsers = new List<UserListModel>();
        foreach (var item in listTotal.list)
        {
            tempUsers.Add(new UserListModel()
            {
                Id = item.Item1.id,
                Avatar = item.Item1.avatar,
                NickName = item.Item1.nickname,
                Remark = item.Item1.remark ?? string.Empty,
                UserName = item.Item1.username,
                DeptId = item.Item3?.id,
                DeptName = item.Item3?.deptname,
                RoleId = item.Item2?.id,
                RoleName = item.Item2?.rolename,
                Email = item.Item1.email ?? string.Empty,
            });
        }
        var result = new PagerResultModel<List<UserListModel>>()
        {
            Items = tempUsers,
            Total = listTotal.total
        };
        return base.Success<PagerResultModel<List<UserListModel>>>(result);
    }

    /// <summary>
    /// 验证用户名是否存在
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> IsAccountExist(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return base.Error<bool>("登录名不能为空");
        var result = await this._userRepository.Select.FirstAsync(p => p.username == userName);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// add a new user account
    /// </summary>
    /// <param name="model">用户实体</param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> AddAccount(AccountRequestModel model)
    {
        var isExist = await IsAccountExist(model.UserName);
        if (isExist.Code != ResultEnum.SUCCESS)
            return isExist;
        if (isExist.Result)
            return base.Error<bool>("登录名已存在");
        var userEntity = new sys_user()
        {
            id = base.CreateId(),
            nickname = model.NickName,
            username = model.UserName,
            password = await _pwdStrategy.GeneratePassword(),
            avatar = "",
            status = 1,
            remark = model.Remark,
            email = model.Email
        };
        var result = await ((SysUserRepository)this._userRepository).AddUserAsync(userEntity, model.RoleId, model.DeptId);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// update a user account information
    /// </summary>
    /// <param name="model">用户实体</param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> UpdateAccount(AccountRequestModel model)
    {
        var entity = await this._userRepository.Select.Where(p => p.id.Equals(model.Id)).FirstAsync();
        if (null == entity)
            return base.Error<bool>("用户不存在");
        if(!model.UserName.Equals(entity.username))
        {
            var isExist = await IsAccountExist(model.UserName);
            if(isExist.Code != ResultEnum.SUCCESS)
                return base.Error<bool>("登录名已存在");
        }
        // entity.username = model.UserName;
        entity.nickname = model.NickName;
        entity.email = model.Email;
        entity.remark = model.Remark;
        if (!string.IsNullOrEmpty(model.Remark))
            entity.remark = model.Remark;
        var result = await ((SysUserRepository)this._userRepository).UpdateUserAsync(entity, model.RoleId, model.DeptId);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// remove a account
    /// </summary>
    /// <param name="userId">用户唯一标识</param>
    /// <returns></returns>
    public async Task<ResultModel<bool>> RemoveDept(string userId)
    {
        var result = await ((SysUserRepository)this._userRepository).RemoveUserAsync(userId);
        return base.Success<bool>(result);
    }

    /// <summary>
    /// 获取当前登录用户权限代码集合
    /// </summary>
    /// <param name="userId">用户唯一标识</param>
    /// <returns></returns>
    public async Task<ResultModel<IEnumerable<string>>> GetPermCode(string userId)
    {
        var result = await ((SysUserRepository)this._userRepository).GetPremCodesAsync(userId);
        return base.Success<IEnumerable<string>>(result);
    }

    /// <summary>
    /// 获取当前登录用户api权限验证集合
    /// </summary>
    /// <param name="userid">用户唯一标识</param>
    /// <returns></returns>
    public async Task<ResultModel<ApiPermissionModel>> GetApiPermCode(string userid)
    {
        var checkResult = await((SysUserRepository)this._userRepository).GetApiPermCode(userid);
        ApiPermissionModel result = new ApiPermissionModel()
        {
            CheckApi = checkResult.checkApi,
            Apis = checkResult.apis
        };
        return base.Success<ApiPermissionModel>(result);
    }
}
