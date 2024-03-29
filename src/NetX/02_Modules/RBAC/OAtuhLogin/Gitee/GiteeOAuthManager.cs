﻿using Authentication.OAuth;
using NetX.Common.Attributes;
using NetX.RBAC.Models.Dtos.RequestDto;
using NetX.RBAC.OAtuhLogin;

namespace NetX.RBAC.Domain
{
    [Scoped]
    public class GiteeOAuthManager : IOAuthManager<DefaultAccessTokenModel, GiteeUserModel>, IOAuthManager
    {
        private readonly GiteeOAuth _giteeOAuth;

        public OAuthPlatform Platform => OAuthPlatform.Gitee;

        public GiteeOAuthManager(GiteeOAuth giteeOAuth)
        {
            this._giteeOAuth = giteeOAuth;
        }

        /// <summary>
        /// 获取授权url
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<string> GetOAuthUrl(OAuthModel model)
        {
            return await _giteeOAuth.GetAuthorizeUrl(model.State);
        }

        /// <summary>
        /// 授权结果
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<OAuthResult<DefaultAccessTokenModel, GiteeUserModel>> GetOAuthResult(Dictionary<string, string> param)
        {
            var oAuthResult = await _giteeOAuth.GetOAuth(param);
            if (null == oAuthResult || null == oAuthResult.AccessToken || null == oAuthResult.UserInfo)
                throw new Exception("授权失败");
            return oAuthResult;
        }
    }
}
