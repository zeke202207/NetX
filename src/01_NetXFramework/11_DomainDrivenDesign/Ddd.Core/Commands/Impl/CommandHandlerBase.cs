namespace Netx.Ddd.Core;

/// <summary>
/// Defines a Command handler interface
/// </summary>
public abstract class CommandHandlerBase<T> : IRequestHandler<T, bool>
    where T : CommandBase
{
    public abstract Task<bool> Handle(T request, CancellationToken cancellationToken);
}
