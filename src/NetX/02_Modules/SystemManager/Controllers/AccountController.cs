using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common.Models;
using NetX.Swagger;
using NetX.SystemManager.Core;
using NetX.SystemManager.Models;
using NetX.Tenants;

namespace NetX.SystemManager.Controllers
{
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
        [ApiActionDescriptionAttribute("登录")]
        [NoPermission]
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var userInfo = await _accoutService.Login(model.UserName, model.Password);
            if (null == userInfo)
                return base.Error(ResultEnum.ERROR, "账号密码验证失败");
            string token = await _accoutService.GetToken(new ClaimModel()
            {
                UserId = userInfo.Id,
                LoginName = userInfo.UserName,
                DisplayName = userInfo.NickName
            });
            if (string.IsNullOrWhiteSpace(token))
                return base.Error(ResultEnum.ERROR, "获取token失败");
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
        /// Token验证并获取用户信息
        /// </summary>
        /// <returns></returns>
        [ApiActionDescriptionAttribute("获取用户信息")]
        [HttpGet]
        public async Task<ActionResult> GetUserInfo()
        {
            var userInfo = await _accoutService.GetUserInfo(TenantContext.CurrentTenant.Principal?.UserId);
            if (null == userInfo)
                return base.Error(ResultEnum.ERROR, "获取用户信息失败");
            return base.Success<UserModel>(userInfo);
        }

        /// <summary>
        /// 获取用户列表集合
        /// </summary>
        /// <param name="userListparam"></param>
        /// <returns></returns>
        [ApiActionDescriptionAttribute("获取用户列表集合")]
        [HttpGet]
        public async Task<ActionResult> GetAccountList([FromQuery] UserListParam userListparam)
        {
            var userList = await _accoutService.GetAccountLists(userListparam);
            if (null == userList)
                return base.Error(ResultEnum.ERROR, "获取用户列表失败");
            return base.Success<List<UserListModel>>(userList);
        }

        /// <summary>
        /// 判断用户是否存在
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [ApiActionDescription("用户名是否存在校验")]
        [HttpGet]
        public async Task<ActionResult> IsAccountExist([FromQuery] string account)
        {
            var isExist = await _accoutService.IsAccountExist(account);
            if (isExist)
                return base.Error(ResultEnum.ERROR, "用户名存在");
            return base.Success<bool>(isExist);
        }

        /// <summary>
        /// 获取登录用户访问权限code集合
        /// v-auth="'menu:zeke'"
        /// </summary>
        /// <returns></returns>
        [ApiActionDescriptionAttribute("获取用户按钮权限集合")]
        [HttpGet]
        public async Task<ActionResult> GetPermCode()
        {
            var result = await _accoutService.GetPermCode(TenantContext.CurrentTenant.Principal?.UserId ?? string.Empty);
            return base.Success<IEnumerable<string>>(result);
        }

        /// <summary>
        /// 等处系统
        /// </summary>
        /// <returns></returns>
        [ApiActionDescriptionAttribute("登出系统")]
        [HttpGet]
        public ActionResult Logout()
        {
            return base.Success<bool>(true);
        }

        /// <summary>
        /// add a new user account
        /// </summary>
        /// <returns></returns>
        [ApiActionDescription("添加用户")]
        [HttpPost]
        public async Task<ActionResult> AddAccount(AccountRequestModel model)
        {
            var result = await _accoutService.AddAccount(model);
            return new JsonResult(new ResultModel<bool>(ResultEnum.SUCCESS)
            {
                Result = result
            });
        }

        /// <summary>
        /// edit a new user account
        /// </summary>
        /// <returns></returns>
        [ApiActionDescription("修改用户")]
        [HttpPost]
        public async Task<ActionResult> UpdateAccount(AccountRequestModel model)
        {
            var result = await _accoutService.UpdateAccount(model);
            return new JsonResult(new ResultModel<bool>(ResultEnum.SUCCESS)
            {
                Result = result
            });
        }

        /// <summary>
        /// remove a user account
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [ApiActionDescriptionAttribute("删除用户")]
        [HttpDelete]
        public async Task<ActionResult> RemoveAccount(DeleteParam param)
        {
            var result = await _accoutService.RemoveDept(param.Id);
            return new JsonResult(new ResultModel<bool>(ResultEnum.SUCCESS)
            {
                Result = result
            });
        }
    }
}
