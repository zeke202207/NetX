using Microsoft.EntityFrameworkCore.ChangeTracking;
using Netx.Ddd.Core;
using NetX.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Netx.Ddd.Domain.Aggregates;
using NetX.Common.Attributes;
using NetX.Common;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Concurrent;
using System.Data;

namespace Netx.Ddd.Domain;

public class UnitOfWork : IUnitOfWork
{
    private readonly IEventBus _eventBus;
    private readonly NetxContext _dbContext;

    public UnitOfWork(IEventBus eventBus, NetxContext dbContext)
    {
        _eventBus = eventBus;
        _dbContext = dbContext;
    }

    /// <summary>
    /// 提交事务
    /// </summary>
    /// <returns></returns>
    public async Task<bool> CommitAsync()
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        await _eventBus.PublishDomainEvents(_dbContext).ConfigureAwait(false);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        var success = await _dbContext.SaveChangesAsync() > 0;

        return success;
    }

    /// <summary>
    /// Creates a Microsoft.EntityFrameworkCore.DbSet`1 that can be used to query and save instances of TEntity.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <returns></returns>
    public DbSet<T> GetRepository<T,TKey>() where T : BaseEntity<TKey>
    {
        return _dbContext.Set<T>();
    }

    /// <summary>
    /// 释放数据库链接资源
    /// </summary>
    public void Dispose()
    {
        _dbContext?.Dispose();
    }
}
