using Authentication.OAuth;
using Netx.Ddd.Core;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Domain.Queries;
using NetX.RBAC.Models;
using NetX.RBAC.Models.Dtos.ReponseDto;
using NetX.RBAC.Models.Dtos.RequestDto;
using NetX.RBAC.OAtuhLogin.Gitee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain.Core
{
    /// <summary>
    /// TODO:多注入区分，支持多系统接入
    /// </summary>
    [Scoped]
    public class OAuthCore : IOAuthCore
    {
        private readonly IOAuthManager<DefaultAccessTokenModel, GiteeUserModel> _giteeManager;
        private readonly IQueryBus _accountQuery;

        public OAuthCore(IOAuthManager<DefaultAccessTokenModel, GiteeUserModel> giteeManager, IQueryBus accountQuery)
        {
            this._giteeManager = giteeManager;
            this._accountQuery = accountQuery;
        }

        /// <summary>
        /// 获取第三方登录地址
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> GetOAuthUrl(OAuthModel model)
        {
            return await this._giteeManager.GetOAuthUrl(model);
        }

        /// <summary>
        /// 第三方登录验证
        /// </summary>
        /// <param name="oAuthLoginModel"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ResultModel> OAuthLogin(OAuthLoginModel oAuthLoginModel)
        {
            var oAuthResult = await _giteeManager.GetOAuthResult(new Dictionary<string, string>() { { "code", oAuthLoginModel.Code } });
            var userId = oAuthResult.UserInfo.Id;
            //数据库查询userid
            //以下为测试代码，gitee登录默认为超级管理员，请根据业务进行调整
            OAuthLoginResult result = new OAuthLoginResult() { Result = OAuthResult.NotBinding };
            if (userId == "452536")
            {
                var loginResult = await this._accountQuery.Send<LoginQuery, ResultModel>(new LoginQuery("zeke", "123456")) as ResultModel<LoginResult>;
                result.LoginResult = loginResult.Result;
                result.Result = OAuthResult.Success;
            }
            return result.ToSuccessResultModel();
        }
    }
}
