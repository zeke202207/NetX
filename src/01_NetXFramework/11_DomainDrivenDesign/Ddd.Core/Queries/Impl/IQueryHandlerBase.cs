namespace NetX.Ddd.Core;

/// <summary>
/// Defines a QueryHandler interface
/// </summary>
public abstract class IQueryHandlerBase<TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : QueryBase<TResponse>
{
    public abstract Task<TResponse> Handle(TQuery request, CancellationToken cancellationToken);
}
