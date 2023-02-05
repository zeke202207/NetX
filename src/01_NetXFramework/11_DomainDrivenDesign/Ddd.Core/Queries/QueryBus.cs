namespace Netx.Ddd.Core;

public class QueryBus : IQueryBus
{
    private readonly IMediator _mediator;

    public QueryBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TResponse> Send<TQuery, TResponse>(TQuery query) where TQuery : QueryBase<TResponse>
    {
        return _mediator.Send(query);
    }
}
