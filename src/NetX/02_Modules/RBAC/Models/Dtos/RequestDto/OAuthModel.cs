using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Models.Dtos.RequestDto
{
    public class OAuthModel
    {
        public OAuthType AuthType { get; set; }

        public string State { get; set; } = "code";
    }
}
