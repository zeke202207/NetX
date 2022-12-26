﻿using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common.ModuleInfrastructure;
using NetX.Swagger;
using NetX.RBAC.Core;
using NetX.RBAC.Models;
using NetX.Tenants;
using NetX.Logging.Monitors;
using NetX.Common;
using Netx.Ddd.Core;
using NetX.RBAC.Domain;

namespace NetX.RBAC.Controllers;

/// <summary>
/// 账号管理api接口
/// </summary>
[ApiControllerDescription(RBACConst.C_RBAC_GROUPNAME, Description = "NetX实现的系统管理模块->账号管理")]
public class AccountController : RBACBaseController
{
    private readonly IAccountService _accoutService;
    private readonly IQueryBus _accountQuery;
    private readonly ICommandBus _accountCommand;

    /// <summary>
    /// 账号管理api实例对象
    /// </summary>
    /// <param name="accountService">账户管理服务</param>
    public AccountController(IAccountService accountService, IQueryBus accountQuery, ICommandBus accountCommand)
    {
        this._accoutService = accountService;
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
        //this._accountCommand.Send<TestCommand>(new TestCommand(Guid.NewGuid(), DateTime.Now, "zeke"));
        return await _accoutService.Login(model.UserName, model.Password);
    }

    /// <summary>
    /// 获取登录用户信息
    /// </summary>
    /// <returns></returns>
    [ApiActionDescription("获取登录用户信息")]
    [HttpGet]
    public async Task<ResultModel> GetUserInfo()
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
    //[ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
    public async Task<ResultModel> GetAccountList([FromQuery] UserListParam userListparam)
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
    public async Task<ResultModel> IsAccountExist([FromQuery] string account)
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
    public async Task<ResultModel> GetPermCode()
    {
        return await _accoutService.GetPermCode(TenantContext.CurrentTenant.Principal?.UserId ?? string.Empty);
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
    public async Task<ResultModel> UpdateAccount(AccountRequestModel model)
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
    public async Task<ResultModel> RemoveAccount(KeyParam param)
    {
        return await _accoutService.RemoveDept(param.Id);
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
        return await _accoutService.ChangePassword(TenantContext.CurrentTenant.Principal?.UserId ?? string.Empty, model);
    }
}
