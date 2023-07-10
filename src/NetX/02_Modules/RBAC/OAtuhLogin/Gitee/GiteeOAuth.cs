using Authentication.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.OAtuhLogin.Gitee
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
