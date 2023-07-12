using NetX.Common.ModuleInfrastructure;
using NetX.RBAC.Models;
using NetX.RBAC.Models.Dtos.ReponseDto;
using NetX.RBAC.Models.Dtos.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain
{
    public interface IOAuthCore
    {
        Task<string> GetOAuthUrl(OAuthModel model);

        Task<ResultModel> OAuthLogin(OAuthLoginModel oAuthLoginModel);
    }
}
