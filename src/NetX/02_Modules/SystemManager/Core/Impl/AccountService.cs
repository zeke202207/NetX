using AutoMapper;
using FreeSql;
using NetX.Authentication.Core;
using NetX.Common;
using NetX.Common.Attributes;
using NetX.SystemManager.Data.Repositories;
using NetX.SystemManager.Models;

namespace NetX.SystemManager.Core;

/// <summary>
/// 账号管理服务
/// </summary>
[Scoped]
public class AccountService : BaseService, IAccountService
{
    private readonly IBaseRepository<sys_user> _userRepository;
    private readonly IEncryption _encryption;
    private readonly ILoginHandler _loginHandler;
    private readonly IMapper _mapper;

    /// <summary>
    /// 账号管理服务实例对象
    /// </summary>
    /// <param name="userRepository"></param>
    /// <param name="encryption"></param>
    /// <param name="loginHandler"></param>
    public AccountService(
        IBaseRepository<sys_user> userRepository,
        IEncryption encryption,
        ILoginHandler loginHandler,
        IMapper mapper)
    {
        this._userRepository = userRepository;
        this._encryption = encryption;
        this._loginHandler = loginHandler;
        this._mapper = mapper;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="username"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    public async Task<UserModel> Login(string username, string password)
    {
        var user = await this._userRepository.Select.Where(p => p.username.Equals(username)).FirstAsync<sys_user>();
        if (null == user || _encryption.Encryption(password).ToLower().Equals(user.password))
            return default(UserModel);
       return this._mapper.Map<UserModel>(user);
    }

    /// <summary>
    /// 获取Token信息
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<string> GetToken(ClaimModel model)
    {
        var result = this._loginHandler.Handle(model, string.Empty);
        if (null != result)
            return await Task.FromResult(result.AccessToken);
        return await Task.FromResult(string.Empty);
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<UserModel> GetUserInfo(string userId)
    {
        if (string.IsNullOrEmpty(userId))
            return default(UserModel);
        var user = await this._userRepository.Select.Where(p => p.id.Equals(userId)).FirstAsync<sys_user>();
        if (null == user)
            return default(UserModel);
        return new UserModel()
        {
            Id = user.id,
            Avatar = user.avatar,
            UserName = user.username,
            NickName = user.nickname,
            Remark = user.remark ?? string.Empty,
            Status = user.status.ToString()
        };
    }

    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <param name="userParam"></param>
    /// <returns></returns>
    public async Task<List<UserListModel>> GetAccountLists(UserListParam userParam)
    {
        var list = await ((SysUserRepository)_userRepository).GetUserList(userParam.DeptId, userParam.Account, userParam.Nickname, userParam.Page, userParam.PageSize);
        List<UserListModel> result = new List<UserListModel>();
        foreach (var item in list)
        {
            result.Add(new UserListModel()
            {
                Id = item.Item1.id,
                Avatar = item.Item1.avatar,
                NickName = item.Item1.nickname,
                Remark = item.Item1.remark??string.Empty,
                UserName = item.Item1.username,
                DeptId = item.Item3?.id,
                DeptName = item.Item3?.deptname,
                RoleId = item.Item2?.id,
                RoleName = item.Item2?.rolename,
                Email = item.Item1.email??string.Empty,
            });
        }
        return result;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public async Task<bool> IsAccountExist(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            return false;
        return await this._userRepository.Select.FirstAsync(p => p.username == userName);
    }

    /// <summary>
    /// add a new user account
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<bool> AddAccount(AccountRequestModel model)
    {
        var isExist = await IsAccountExist(model.UserName);
        if (isExist)
            return false;
        var userEntity = new sys_user()
        {
            id = base.CreateId(),
            nickname = model.NickName,
            username = model.UserName,
            password = "zeke",
            avatar = "",
            status = 1,
            remark = model.Remark,
            email = model.Email
        };
        return await ((SysUserRepository)this._userRepository).AddUser(userEntity, model.RoleId, model.DeptId);
    }

    /// <summary>
    /// update a user account information
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    public async Task<bool> UpdateAccount(AccountRequestModel model)
    {
        var entity = await this._userRepository.Select.Where(p => p.id.Equals(model.Id)).FirstAsync();
        if (null == entity)
            return false;
        entity.username = model.UserName;
        entity.nickname = model.NickName;
        entity.email = model.Email;
        entity.remark = model.Remark;
        if (!string.IsNullOrEmpty(model.Remark))
            entity.remark = model.Remark;
        return await ((SysUserRepository)this._userRepository).UpdateUser(entity, model.RoleId, model.DeptId);

    }

    /// <summary>
    /// remove a account
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<bool> RemoveDept(string id)
    {
        return await ((SysUserRepository)this._userRepository).RemoveUser(id);
    }

    /// <summary>
    /// 获取当前登录用户权限代码集合
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<IEnumerable<string>> GetPermCode(string userId)
    {
        return await ((SysUserRepository)this._userRepository).GetPremCodes(userId);
    }
}
