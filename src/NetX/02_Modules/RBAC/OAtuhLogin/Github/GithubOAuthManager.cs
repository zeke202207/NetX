using Authentication.OAuth;
using NetX.Common.Attributes;
using NetX.RBAC.Models.Dtos.RequestDto;
using NetX.RBAC.OAtuhLogin;

namespace NetX.RBAC.Domain
{
    [Scoped]
    public class GithubOAuthManager : IOAuthManager<DefaultAccessTokenModel, GithubUserModel>, IOAuthManager
    {
        private readonly GithubOAuth _githubOAuth;

        public GithubOAuthManager(GithubOAuth githubOAuth)
        {
            this._githubOAuth = githubOAuth;
        }

        public OAuthPlatform Platform => OAuthPlatform.Github;

        public async Task<OAuthResult<DefaultAccessTokenModel, GithubUserModel>> GetOAuthResult(Dictionary<string, string> param)
        {
            var oAuthResult = await _githubOAuth.GetOAuth(param);
            if (null == oAuthResult || null == oAuthResult.AccessToken || null == oAuthResult.UserInfo)
                throw new Exception("授权失败");
            return oAuthResult;
        }

        public async Task<string> GetOAuthUrl(OAuthModel model)
        {
            return await _githubOAuth.GetAuthorizeUrl(model.State);
        }
    }
}
