namespace Netx.Ddd.Domain;

public sealed class EventStoreSQLContext : DbContext
{
    public EventStoreSQLContext(DbContextOptions<EventStoreSQLContext> options) 
        : base(options) 
    { }

    public DbSet<StoredEvent> StoredEvent { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
