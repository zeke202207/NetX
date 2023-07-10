using Authentication.OAuth;
using NetX.Common.Attributes;
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

        public OAuthCore(IOAuthManager<DefaultAccessTokenModel, GiteeUserModel> giteeManager)
        {
            this._giteeManager = giteeManager;
        }

        public async Task<string> GetOAuthUrl(OAuthModel model)
        {
            return await this._giteeManager.GetOAuthUrl(model);
        }

        public async Task<OAuthLoginResultModel> GerRedirctUrl(Dictionary<string, string> param)
        {
            var oAuthResult = await _giteeManager.GetOAuthResult(param);
            //1.查询系统是否包含oauth系统与本系统关联账号

            //1.1 存在关联账号，获取关联账号用户信息，生成登录token，跳转登录成功界面

            //1.2 不存在关联账号，跳转到绑定账号页面,进行绑定，绑定成功后，生成token，跳转登录成功页面

            return new OAuthLoginResultModel();
        }
    }
}
