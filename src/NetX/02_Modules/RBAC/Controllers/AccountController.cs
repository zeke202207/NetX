using Microsoft.AspNetCore.Mvc;
using Netx.Ddd.Core;
using NetX.Authentication.Core;
using NetX.Common;
using NetX.Common.ModuleInfrastructure;
using NetX.Logging.Monitors;
using NetX.RBAC.Domain;
using NetX.RBAC.Domain.Queries;
using NetX.RBAC.Models;
using NetX.Swagger;
using NetX.Tenants;

namespace NetX.RBAC.Controllers;

/// <summary>
/// 账号管理api接口
/// </summary>
[ApiControllerDescription(RBACConst.C_RBAC_GROUPNAME, Description = "NetX实现的系统管理模块->账号管理")]
public class AccountController : RBACBaseController
{
    private readonly IQueryBus _accountQuery;
    private readonly ICommandBus _accountCommand;

    /// <summary>
    /// 账号管理api实例对象
    /// </summary>
    /// <param name="accountService">账户管理服务</param>
    public AccountController(IQueryBus accountQuery, ICommandBus accountCommand)
    {
        this._accountQuery = accountQuery;
        this._accountCommand = accountCommand;
    }

    /// <summary>
    /// 系统登录
    /// </summary>
    /// <param name="model">登录系统实体对象</param>
    /// <returns></returns>
    [ApiActionDescription("登录")]
    [NoPermission]
    [HttpPost]
    public async Task<ResultModel> Login(LoginModel model)
    {
        return await this._accountQuery.Send<LoginQuery, ResultModel>(new LoginQuery(model.UserName, model.Password));
    }

    /// <summary>
    /// 获取登录用户信息
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("获取登录用户信息")]
    [HttpGet]
    public async Task<ResultModel> GetUserInfo()
    {
        return await this._accountQuery.Send<LoginUserInfoQuery, ResultModel>(new LoginUserInfoQuery(TenantContext.CurrentTenant.Principal?.UserId??string.Empty));
    }

    /// <summary>
    /// 获取用户列表分页数据集合
    /// </summary>
    /// <param name="userListparam">筛选条件</param>
    /// <returns></returns>
    [ApiActionDescription("获取用户列表分页数据集合")]
    [HttpGet]
    //[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
    public async Task<ResultModel> GetAccountList([FromQuery] UserListParam userListparam)
    {
        return await _accountQuery.Send<AccountListQuery,ResultModel>(new AccountListQuery(userListparam.DeptId,userListparam.Account,userListparam.Nickname,userListparam.CurrentPage, userListparam.PageSize)); ;
    }

    ///// <summary>
    ///// 判断用户是否存在
    ///// </summary>
    ///// <param name="account">用户名</param>
    ///// <returns></returns>
    //[ApiActionDescription("用户名是否存在校验")]
    //[HttpGet]
    //public async Task<ResultModel> IsAccountExist([FromQuery] string account)
    //{
    //    return await _accoutService.IsAccountExist(account);
    //}

    /// <summary>
    /// 获取登录用户访问权限code集合
    /// v-auth="'menu:zeke'"
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("获取用户按钮权限集合")]
    [HttpGet]
    public async Task<ResultModel> GetPermCode()
    {
        return await _accountQuery.Send<AccountPermCodeQuery, ResultModel>(new AccountPermCodeQuery(TenantContext.CurrentTenant.Principal?.UserId ?? string.Empty));
    }

    /// <summary>
    /// 登出系统
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("登出系统")]
    [HttpGet]
    public async Task<ResultModel> Logout()
    {
        return await Task.FromResult(new ResultModel<bool>(ResultEnum.SUCCESS) { Result = true });
    }

    /// <summary>
    /// 注册添加新用户
    /// </summary>
    /// <param name="model">用户信息对象</param>
    /// <returns></returns>
    [ApiActionDescription("注册添加新用户")]
    [HttpPost]
    public async Task<ResultModel> AddAccount(AccountRequestModel model)
    {
        await _accountCommand.Send<AccountAddCommand>(new AccountAddCommand(model.UserName,model.NickName,model.RoleId,model.DeptId,model.Email,model.Remark));
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 编辑用户信息
    /// </summary>
    /// <param name="model">用户信息实体对象</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("编辑用户信息")]
    [HttpPost]
    public async Task<ResultModel> UpdateAccount(AccountRequestModel model)
    {
        await _accountCommand.Send<AccountEditCommand>(new AccountEditCommand(model.Id, model.UserName, model.NickName, model.RoleId, model.DeptId, model.Email, model.Remark));
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="param">删除参数</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("删除用户")]
    [HttpDelete]
    public async Task<ResultModel> RemoveAccount(KeyParam param)
    {
        await _accountCommand.Send<AccountRemoveCommand>(new AccountRemoveCommand(param.Id));
        return true.ToSuccessResultModel();
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("修改密码")]
    [HttpPost]
    public async Task<ResultModel> ChangePassword(ChangePwdRequestModel model)
    {
        await _accountCommand.Send<AccountModifyPwdCommand>(new AccountModifyPwdCommand(TenantContext.CurrentTenant.Principal?.UserId ?? string.Empty, model.NewPassword, model.NewPassword));
        return true.ToSuccessResultModel();
    }
}
