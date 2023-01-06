using Netx.Ddd.Domain;
using NetX.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC;

public class RbacException : ExceptionBase
{
    public RbacException(string message, int statusCode) 
        : base(message, statusCode)
    {
    }
}
