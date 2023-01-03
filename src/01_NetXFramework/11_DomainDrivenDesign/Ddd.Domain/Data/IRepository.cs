using Netx.Ddd.Domain.Aggregates;

namespace Netx.Ddd.Domain;

public class BaseRepository<T, TKey> where T : BaseEntity<TKey>
{
    public DbContext Db { get; protected set; }

    public void SetDbContext(DbContext db) => Db = db;
}
