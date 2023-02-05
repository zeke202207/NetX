using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Common;

public class ExceptionBase : Exception
{
    public int StatusCode { get; set; }

    public ExceptionBase(string message, int statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }
}
