namespace Netx.Ddd.Domain;

public interface IUnitOfWork
{
    Task<bool> CommitAsync();
}
