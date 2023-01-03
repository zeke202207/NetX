namespace Netx.Ddd.Domain;

public abstract class DomainQueryHandler<TQuery, TResponse> : IQueryHandlerBase<TQuery, TResponse>
    where TQuery : QueryBase<TResponse>
{
    protected readonly IDatabaseContext _dbContext;

    public DomainQueryHandler(IDatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }
}
