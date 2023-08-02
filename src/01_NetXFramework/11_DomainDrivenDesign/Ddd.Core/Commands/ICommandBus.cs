namespace NetX.Ddd.Core
{
    public interface ICommandBus
    {
        Task Send<TCommand>(TCommand command) where TCommand : CommandBase;
    }
}
