namespace NetX.SharedFramework.ChainPipeline;

/// <summary>
/// 管道传递参数
/// </summary>
public sealed class RequestContext<T>
{
    public readonly CancellationTokenSource Cts;

    public T Parameter { get; private set; }

    public CancellationToken CancellationToken { get; }

    public RequestContext(T param)
    {
        Parameter = param;
        Cts = new CancellationTokenSource();
        CancellationToken = Cts.Token;
    }
}
