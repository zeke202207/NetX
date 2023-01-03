using Microsoft.AspNetCore.Mvc;
using Netx.Ddd.Core;
using NetX.Authentication.Core;
using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Domain;
using NetX.RBAC.Models;
using NetX.Swagger;

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
        await this._accountCommand.Send<TestCommand>(new TestCommand(Guid.NewGuid(), DateTime.Now, "zeke"));
        var result = await this._accountQuery.Send<TestQuery, string>(new TestQuery("zeke"));
        return null;
    }
}
