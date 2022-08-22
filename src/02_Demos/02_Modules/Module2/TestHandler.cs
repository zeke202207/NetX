using Microsoft.AspNetCore.Http;
using NetX.Authentication;
using NetX.Authentication.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2
{
    public class TestHandler : IPermissionValidateHandler
    {
        public bool Validate(HttpContext context, IDictionary<string, string> routeValues)
        {
            return true;
        }
    }
}
