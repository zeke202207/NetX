using Authentication.OAuth;
using NetX.RBAC.Models.Dtos.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain
{
    public interface IOAuthManager<TAccessTokenModel, TUserInfoModel>
        where TAccessTokenModel : AccessTokenSuccessModel
        where TUserInfoModel : UserInfoModelBase
    {
        Task<string> GetOAuthUrl (OAuthModel model);

        Task<OAuthResult<TAccessTokenModel, TUserInfoModel>> GetOAuthResult(Dictionary<string, string> param);
    }
}
