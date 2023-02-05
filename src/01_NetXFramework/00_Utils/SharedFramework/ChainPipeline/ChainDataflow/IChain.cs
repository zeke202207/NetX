namespace NetX.SharedFramework.ChainPipeline.ChainDataflow;

/// <summary>
/// 
/// </summary>
public interface IChain<TParameter, TResult>
        where TParameter : DataflowParameter
        where TResult : DataflowResult
{
    /// <summary>
    /// Chains a new middleware to the chain of responsibility.
    /// Middleware will be executed in the same order they are added.
    /// </summary>
    /// <typeparam name="TMiddleware">The new middleware being added.</typeparam>
    /// <returns>The current instance of <see cref="IChain{TParameter, TReturn}"/>.</returns>
    IChain<TParameter, TResult> Add<TMiddleware>()
        where TMiddleware : IChainMiddleware<TParameter, TResult>;

    /// <summary>
    /// Chains a new middleware type to the chain of responsibility.
    /// Middleware will be executed in the same order they are added.
    /// </summary>
    /// <param name="middlewareType">The middleware type to be executed.</param>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="middlewareType"/> is 
    /// not an implementation of <see cref="IChainMiddleware{TParameter, TReturn}"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="middlewareType"/> is null.</exception>
    /// <returns>The current instance of <see cref="IChain{TParameter, TReturn}"/>.</returns>
    IChain<TParameter, TResult> Add(Type middlewareType);

    /// <summary>
    /// Executes the configured chain of responsibility.
    /// </summary>
    /// <param name="parameter"></param>
    TResult Execute(TParameter parameter);
}
