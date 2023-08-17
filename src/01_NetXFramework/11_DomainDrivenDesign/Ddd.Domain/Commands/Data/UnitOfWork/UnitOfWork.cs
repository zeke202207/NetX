using Microsoft.EntityFrameworkCore.Storage;
using NetX.Ddd.Domain.Aggregates;
using System.Reflection;

namespace NetX.Ddd.Domain;

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
    /// 提交修改
    /// </summary>
    /// <returns></returns>
    public async Task<bool> SaveChangesAsync()
    {
        return await SaveChangesAsync(true);
    }

    /// <summary>
    /// 提交修改
    /// </summary>
    /// <param name="publish"></param>
    /// <returns></returns>
    public async Task<bool> SaveChangesAsync(bool publish)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        if (publish)
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
    public DbSet<T> GetRepository<T, TKey>() where T : BaseEntity<TKey>
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

    /// <summary>
    /// 开启事务
    /// </summary>
    /// <returns></returns>
    public Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return _dbContext.Database.BeginTransactionAsync();
    }

    /// <summary>
    /// 关闭事务
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (null == transaction)
            return;
        await transaction.CommitAsync();
        await transaction.DisposeAsync();
    }

    /// <summary>
    /// 事务会馆
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task RollbackAsync(IDbContextTransaction transaction)
    {
        if (null == transaction)
            return;
        await transaction.RollbackAsync();
    }

    /// <summary>
    /// 自动事务
    /// </summary>
    /// <param name="actions"></param>
    /// <returns></returns>
    public async Task<bool> AutoTransaction(params TransactionOption[] options)
    {
        bool result = true;
        using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var method = this._dbContext.GetType().GetMethod("Set", new Type[] { });
            var total = options.Length;
            int i = 0;
            foreach (var option in options)
            {
                MethodInfo curMethod = method.MakeGenericMethod(option.EntityType);
                var dbset = curMethod.Invoke(this._dbContext, null);
                option.DataBaseInvoke.Invoke(dbset);
                await SaveChangesAsync(++i == total);
            }
            await transaction.CommitAsync();
            return result;
        }
        catch (Exception ex)
        {
            result = false;
            throw new Exception("事务失败", ex);
        }
        finally
        {
            if (null != transaction)
            {
                if (!result)
                    await transaction.RollbackAsync();
            }
        }
    }
}
