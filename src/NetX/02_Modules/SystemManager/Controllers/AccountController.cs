using Microsoft.AspNetCore.Mvc;
using NetX.Authentication.Core;
using NetX.Common.Models;
using NetX.Swagger;
using NetX.SystemManager.Core;
using NetX.SystemManager.Models;
using NetX.Tenants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiControllerDescriptionAttribute("SystemManager", Description = "NetX实现的系统管理模块->账号管理")]
    [PermissionValidate]
    public class AccountController : SystemManagerBaseController
    {
        private readonly IAccountService _accoutService;

        public AccountController(IAccountService accountService)
        {
            this._accoutService = accountService;
        }

        /// <summary>
        /// 获取访问Token
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
                UserId = userInfo.UserId, 
                LoginName = userInfo.UserName, 
                DisplayName = userInfo.NickName
            });
            if(string.IsNullOrWhiteSpace(token))
                return base.Error(ResultEnum.ERROR, "获取token失败");
            return base.Success<LoginResult>(new LoginResult()
            {
                UserId = userInfo.UserId,
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
            var userInfo = await _accoutService.GetUserInfo(TenantContext.CurrentTenant.Principal.UserId);
            if (null == userInfo)
                return base.Error(ResultEnum.ERROR, "获取用户信息失败");
            return base.Success<UserModel>(userInfo);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleListparam"></param>
        /// <returns></returns>
        [ApiActionDescriptionAttribute("获取用户列表集合")]
        [HttpGet]
        public async Task<ActionResult> GetAccountList([FromQuery]UserListParam userListparam)
        {
            var userList = await _accoutService.GetAccountLists(userListparam);
            if (null == userList)
                return base.Error(ResultEnum.ERROR, "获取用户列表失败");
            return base.Success<List<UserListModel>>(userList);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [ApiActionDescription("用户名是否存在校验")]
        [HttpGet]
        public async Task<ActionResult> IsAccountExist([FromQuery]string account)
        {
            var isExist = await _accoutService.IsAccountExist(account);
            if (isExist)
                return base.Error(ResultEnum.ERROR, "用户名存在");
            return base.Success<bool>(isExist);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ApiActionDescriptionAttribute("获取用户按钮权限集合")]
        [HttpGet]
        public ActionResult GetPermCode()
        {
            return new JsonResult(new ResultModel<List<string>>(ResultEnum.SUCCESS)
            {
                Result = new List<string>()
                {
                    "0",
                    "1"
                }
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [ApiActionDescriptionAttribute("登出系统")]
        [HttpGet]
        public ActionResult Logout()
        {
            return base.Success<bool>(true);
        }
    }
}
