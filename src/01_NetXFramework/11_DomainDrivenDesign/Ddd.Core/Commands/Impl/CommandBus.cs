namespace Netx.Ddd.Core;

public class CommandBus : ICommandBus
{
    private readonly IMediator _mediator;

    public CommandBus(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task Send<TCommand>(TCommand command) where TCommand : CommandBase
    {
        return _mediator.Send(command);
    }
}
