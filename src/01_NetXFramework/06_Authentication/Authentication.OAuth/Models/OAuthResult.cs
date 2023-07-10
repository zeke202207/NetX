using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.OAuth
{
    public class OAuthResult<TAccessTokenModel, TUserInfoModel>
        where TAccessTokenModel : AccessTokenSuccessModel
        where TUserInfoModel : UserInfoModelBase
    {
        public TAccessTokenModel AccessToken { get; private set; }

        public TUserInfoModel UserInfo { get; private set; }

        public OAuthResult(TAccessTokenModel accessToken, TUserInfoModel userInfo)
        {
            AccessToken = accessToken;
            UserInfo = userInfo;
        }
    }
}
