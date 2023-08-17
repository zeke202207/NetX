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
