namespace Netx.Ddd.Domain;

public abstract class DomainQueryHandler<TQuery, TResponse> : IQueryHandlerBase<TQuery, TResponse>
    where TQuery : QueryBase<TResponse>
{

    public DomainQueryHandler()
    {
    }
}
