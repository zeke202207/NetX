using Microsoft.EntityFrameworkCore.Storage;
using NetX.Ddd.Domain.Aggregates;
using static NetX.Ddd.Domain.UnitOfWork;

namespace NetX.Ddd.Domain;

public interface IUnitOfWork : IDisposable
{
    DbSet<T> GetRepository<T, TKey>() where T : BaseEntity<TKey>;

    Task<bool> SaveChangesAsync();

    /// <summary>
    /// 保存修改
    /// </summary>
    /// <param name="publish">是否发布到事件总线进行记录</param>
    /// <returns></returns>
    Task<bool> SaveChangesAsync(bool publish);

    /// <summary>
    /// 开启事务
    /// 每次dbset add操作后，需要调用savechange方法
    /// 要不然会出现外键约束问题
    /// </summary>
    /// <returns></returns>
    Task<IDbContextTransaction> BeginTransactionAsync();

    /// <summary>
    /// 提交事务
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
    Task CommitTransactionAsync(IDbContextTransaction transaction);

    /// <summary>
    /// 事务回滚
    /// </summary>
    /// <param name="transaction"></param>
    /// <returns></returns>
    Task RollbackAsync(IDbContextTransaction transaction);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="actions"></param>
    /// <returns></returns>
    Task<bool> AutoTransaction(params TransactionOption[] actions);
}
