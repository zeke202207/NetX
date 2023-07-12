using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Models.Dtos.RequestDto
{
    public class OAuthLoginModel
    {
        public string Code { get; set; }

        public string State { get; set; } = "code";
    }
}
