namespace Netx.Ddd.Core
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : CommandBase;
    }
}
