using NetX.Common;

namespace NetX.RBAC;

public class RbacException : ExceptionBase
{
    public RbacException(string message, int statusCode)
        : base(message, statusCode)
    {
    }
}
