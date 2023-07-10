using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Authentication.OAuth
{
    /// <summary>
    /// 默认AccessToken实现
    /// </summary>
    /// <typeparam name="TUserInfoModel"></typeparam>
    public abstract class DefaultOAuthLogin<TUserInfoModel> : OAuthLoginBase<DefaultAccessTokenModel, TUserInfoModel>
        where TUserInfoModel : UserInfoModelBase
    {
        protected DefaultOAuthLogin(OAuthConfig oAuthConfig) : base(oAuthConfig)
        {

        }
    }
}
