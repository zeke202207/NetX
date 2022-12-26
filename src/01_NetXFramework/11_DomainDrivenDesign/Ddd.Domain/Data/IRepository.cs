namespace Netx.Ddd.Domain;

public interface IRepository<T, TKey> : IDisposable where T : IAggregate<TKey>
{
    IUnitOfWork UnitOfWork { get; }

    public DbContext Db { get; }
}
