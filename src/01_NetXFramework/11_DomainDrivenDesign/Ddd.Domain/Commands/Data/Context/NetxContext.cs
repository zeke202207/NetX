
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Netx.Ddd.Domain.Aggregates;
using NetX.Module;
using NetX.Tenants;
using System.Reflection;

namespace Netx.Ddd.Domain;

public sealed class NetxContext : BaseDbContext
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
                //modelBuilder.Model.AddEntityType(type);
                var upkey = type.GetCustomAttribute<UPKeyAttribute>();
                if (null != upkey)
                    modelBuilder.Entity(type).HasKey(upkey.KeyNames);
                else
                    modelBuilder.Entity(type);
            }
        });

        base.OnModelCreating(modelBuilder);
    }
}
