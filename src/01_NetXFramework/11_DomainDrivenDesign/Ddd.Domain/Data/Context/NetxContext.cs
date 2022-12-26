
using Netx.Ddd.Domain.Aggregates;
using NetX.Module;
using System.Reflection;

namespace Netx.Ddd.Domain;

public sealed class NetxContext : DbContext, IUnitOfWork
{
    private readonly IEventBus _eventBus;
    private readonly IEnumerable<ModuleInitializer> _modules;

    public NetxContext(DbContextOptions<NetxContext> options, IEventBus eventBus, IEnumerable<ModuleInitializer> modules) 
        : base(options)
    {
        _eventBus= eventBus;
        _modules= modules;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.AutoDetectChangesEnabled = false;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _modules.ToList().ForEach(module =>
        {
            var entities = module.GetType().Assembly.GetTypes()
            .Where(type =>
            null != type.BaseType 
            && !type.IsAbstract 
            && type.IsClass
            && type.BaseType.IsGenericType 
            && type.BaseType.GetGenericTypeDefinition() == typeof(BaseEntity<>));
            foreach (var type in entities)
            {
                modelBuilder.Model.AddEntityType(type);
            }
        });

        base.OnModelCreating(modelBuilder);
    }

    public async Task<bool> CommitAsync()
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _eventBus.PublishDomainEvents(this).ConfigureAwait(false);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var success = await SaveChangesAsync() > 0;

        return success;
    }
}
