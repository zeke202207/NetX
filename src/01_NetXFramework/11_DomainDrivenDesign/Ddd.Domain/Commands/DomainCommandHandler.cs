namespace Netx.Ddd.Domain;

public abstract class DomainCommandHandler<TCommand> : CommandHandlerBase<TCommand>
    where TCommand : CommandBase
{
    public DomainCommandHandler()
    {
    }
}
