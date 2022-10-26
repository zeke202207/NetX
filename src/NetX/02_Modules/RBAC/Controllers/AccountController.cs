using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common.Models;
using NetX.Swagger;
using NetX.RBAC.Core;
using NetX.RBAC.Models;
using NetX.Tenants;
using NetX.Logging.Monitors;

namespace NetX.RBAC.Controllers;

/// <summary>
/// 账号管理api接口
/// </summary>
[ApiControllerDescription(RBACConst.C_RBAC_GROUPNAME, Description = "NetX实现的系统管理模块->账号管理")]
public class AccountController : RBACBaseController
{
    private readonly IAccountService _accoutService;

    /// <summary>
    /// 账号管理api实例对象
    /// </summary>
    /// <param name="accountService">账户管理服务</param>
    public AccountController(IAccountService accountService)
    {
        this._accoutService = accountService;
    }

    /// <summary>
    /// 系统登录
    /// </summary>
    /// <param name="model">登录系统实体对象</param>
    /// <returns></returns>
    [ApiActionDescription("登录")]
    [NoPermission]
    [HttpPost]
    public async Task<ResultModel<LoginResult>> Login(LoginModel model)
    {
        return await _accoutService.Login(model.UserName, model.Password);
    }

    /// <summary>
    /// 获取登录用户信息
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("获取登录用户信息")]
    [HttpGet]
    public async Task<ResultModel<UserModel>> GetUserInfo()
    {
        return await _accoutService.GetUserInfo(TenantContext.CurrentTenant.Principal?.UserId);
    }

    /// <summary>
    /// 获取用户列表分页数据集合
    /// </summary>
    /// <param name="userListparam">筛选条件</param>
    /// <returns></returns>
    [ApiActionDescription("获取用户列表分页数据集合")]
    [HttpGet]
    public async Task<ResultModel<PagerResultModel<List<UserListModel>>>> GetAccountList([FromQuery] UserListParam userListparam)
    {
        return await _accoutService.GetAccountLists(userListparam);
    }

    /// <summary>
    /// 判断用户是否存在
    /// </summary>
    /// <param name="account">用户名</param>
    /// <returns></returns>
    [ApiActionDescription("用户名是否存在校验")]
    [HttpGet]
    public async Task<ResultModel<bool>> IsAccountExist([FromQuery] string account)
    {
        return await _accoutService.IsAccountExist(account);
    }

    /// <summary>
    /// 获取登录用户访问权限code集合
    /// v-auth="'menu:zeke'"
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("获取用户按钮权限集合")]
    [HttpGet]
    public async Task<ResultModel<IEnumerable<string>>> GetPermCode()
    {
        return await _accoutService.GetPermCode(TenantContext.CurrentTenant.Principal?.UserId ?? string.Empty);
    }

    /// <summary>
    /// 登出系统
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("登出系统")]
    [HttpGet]
    public async Task<ResultModel<bool>> Logout()
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
    public async Task<ResultModel<bool>> AddAccount(AccountRequestModel model)
    {
        return await _accoutService.AddAccount(model);
    }

    /// <summary>
    /// 编辑用户信息
    /// </summary>
    /// <param name="model">用户信息实体对象</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("编辑用户信息")]
    [HttpPost]
    public async Task<ResultModel<bool>> UpdateAccount(AccountRequestModel model)
    {
        return await _accoutService.UpdateAccount(model);
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="param">删除参数</param>
    /// <returns></returns>
    [Audit]
    [ApiActionDescription("删除用户")]
    [HttpDelete]
    public async Task<ResultModel<bool>> RemoveAccount(KeyParam param)
    {
        return await _accoutService.RemoveDept(param.Id);
    }
}
