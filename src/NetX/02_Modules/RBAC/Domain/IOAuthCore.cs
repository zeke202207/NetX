using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Models.Dtos.RequestDto;

namespace NetX.RBAC.Domain
{
    public interface IOAuthCore
    {
        Task<string> GetOAuthUrl(OAuthModel model);

        Task<ResultModel> OAuthLogin(OAuthLoginModel oAuthLoginModel);
    }
}
