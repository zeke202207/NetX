using Authentication.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
