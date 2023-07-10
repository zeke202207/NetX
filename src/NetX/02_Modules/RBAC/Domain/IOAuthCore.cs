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

        Task<OAuthLoginResultModel> GerRedirctUrl(Dictionary<string, string> param);
    }
}
