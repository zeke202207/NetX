using Authentication.OAuth;

namespace NetX.RBAC.OAtuhLogin
{
    public class GiteeOAuth : DefaultOAuthLogin<GiteeUserModel>
    {
        public GiteeOAuth(OAuthConfig oAuthConfig) : base(oAuthConfig)
        {
        }

        protected override string AuthorizeUrl => "https://gitee.com/oauth/authorize";
        protected override string AccessTokenUrl => "https://gitee.com/oauth/token";
        protected override string UserInfoUrl => "https://gitee.com/api/v5/user";
    }
}
