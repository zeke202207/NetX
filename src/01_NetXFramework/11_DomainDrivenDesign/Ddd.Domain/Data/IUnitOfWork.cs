using Netx.Ddd.Domain.Aggregates;

namespace Netx.Ddd.Domain;

public interface IUnitOfWork : IDisposable
{
    DbSet<T> GetRepository<T,TKey>() where T : BaseEntity<TKey>;

   Task<bool> CommitAsync();
}
