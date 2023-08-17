using Authentication.OAuth;
using NetX.RBAC.Models.Dtos.RequestDto;

namespace NetX.RBAC.Domain
{
    public interface IOAuthManager<TAccessTokenModel, TUserInfoModel>
        where TAccessTokenModel : AccessTokenSuccessModel
        where TUserInfoModel : UserInfoModelBase
    {
        Task<string> GetOAuthUrl(OAuthModel model);

        Task<OAuthResult<TAccessTokenModel, TUserInfoModel>> GetOAuthResult(Dictionary<string, string> param);
    }

    public interface IOAuthManager
    {
        OAuthPlatform Platform { get; }
    }
}
