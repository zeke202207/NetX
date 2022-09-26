using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common.Models;
using NetX.Swagger;
using NetX.SystemManager.Core;
using NetX.SystemManager.Models;
using NetX.Tenants;

namespace NetX.SystemManager.Controllers;

/// <summary>
/// 账号管理api接口
/// </summary>
[ApiControllerDescription("SystemManager", Description = "NetX实现的系统管理模块->账号管理")]
[PermissionValidate]
public class AccountController : SystemManagerBaseController
{
    private readonly IAccountService _accoutService;

    /// <summary>
    /// 账号管理api实例对象
    /// </summary>
    /// <param name="accountService"></param>
    public AccountController(IAccountService accountService)
    {
        this._accoutService = accountService;
    }

    /// <summary>
    /// 系统登录
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("登录")]
    [NoPermission]
    [HttpPost]
    public async Task<ResultModel<LoginResult>> Login(LoginModel model)
    {
        return await _accoutService.Login(model.UserName, model.Password);
    }

    /// <summary>
    /// Token验证并获取用户信息
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("获取用户信息")]
    [HttpGet]
    public async Task<ResultModel<UserModel>> GetUserInfo()
    {
        return await _accoutService.GetUserInfo(TenantContext.CurrentTenant.Principal?.UserId);
    }

    /// <summary>
    /// 获取用户列表集合
    /// </summary>
    /// <param name="userListparam"></param>
    /// <returns></returns>
    [ApiActionDescription("获取用户列表集合")]
    [HttpGet]
    public async Task<ResultModel<PagerResultModel<List<UserListModel>>>> GetAccountList([FromQuery] UserListParam userListparam)
    {
        return await _accoutService.GetAccountLists(userListparam);
    }

    /// <summary>
    /// 判断用户是否存在
    /// </summary>
    /// <param name="account"></param>
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
    /// 等处系统
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("登出系统")]
    [HttpGet]
    public async Task<ResultModel<bool>> Logout()
    {
        return await Task.FromResult(new ResultModel<bool>(ResultEnum.SUCCESS) { Result = true });
    }

    /// <summary>
    /// add a new user account
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("添加用户")]
    [HttpPost]
    public async Task<ResultModel<bool>> AddAccount(AccountRequestModel model)
    {
        return await _accoutService.AddAccount(model);
    }

    /// <summary>
    /// edit a new user account
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("修改用户")]
    [HttpPost]
    public async Task<ResultModel<bool>> UpdateAccount(AccountRequestModel model)
    {
        return await _accoutService.UpdateAccount(model);
    }

    /// <summary>
    /// remove a user account
    /// </summary>
    /// <param name="param"></param>
    /// <returns></returns>
    [ApiActionDescription("删除用户")]
    [HttpDelete]
    public async Task<ResultModel<bool>> RemoveAccount(DeleteParam param)
    {
        return await _accoutService.RemoveDept(param.Id);
    }
}
