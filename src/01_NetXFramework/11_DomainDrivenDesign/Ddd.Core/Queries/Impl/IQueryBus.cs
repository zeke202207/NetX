namespace Netx.Ddd.Core
{
    public interface IQueryBus
    {
        Task<TResponse> Send<TQuery, TResponse>(TQuery query)
            where TQuery : QueryBase<TResponse>;
    }
}
