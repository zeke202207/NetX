using Microsoft.AspNetCore.Http;
using NetX.Authentication.Core;
using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.SystemManager
{
    [Scoped]
    public class PermissionValidateHandler : IPermissionValidateHandler
    {
        public bool Validate(HttpContext context, IDictionary<string, string> routeValues)
        {
            return true;
        }
    }
}
