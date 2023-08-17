using Authentication.OAuth;

namespace NetX.RBAC.OAtuhLogin
{
    public class GithubOAuth : DefaultOAuthLogin<GithubUserModel>
    {
        public GithubOAuth(OAuthConfig oAuthConfig) : base(oAuthConfig)
        {
        }

        protected override string AuthorizeUrl => "";
        protected override string AccessTokenUrl => "";
        protected override string UserInfoUrl => "";
    }
}
