using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Models.Dtos.ReponseDto
{
    public class OAuthLoginResult
    {
        public LoginResult LoginResult { get; set; }

        public OAuthResult Result { get; set; }
    }
}
