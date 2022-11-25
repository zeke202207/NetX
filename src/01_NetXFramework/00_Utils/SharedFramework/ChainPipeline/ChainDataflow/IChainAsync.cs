namespace NetX.SharedFramework.ChainPipeline.ChainDataflow;

/// <summary>
/// Defines the asynchronous chain of responsibility.
/// </summary>
/// <typeparam name="TParameter">The input type for the chain.</typeparam>
/// <typeparam name="TResult">The return type of the chain.</typeparam>
public interface IChainAsync<TParameter, TResult>
        where TParameter : DataflowParameter
        where TResult : DataflowResult
{
    /// <summary>
    /// Chains a new middleware to the chain of responsibility.
    /// Middleware will be executed in the same order they are added.
    /// </summary>
    /// <typeparam name="TMiddleware">The new middleware being added.</typeparam>
    /// <returns>The current instance of <see cref="IChainMiddlewareAsync{TParameter, TReturn}"/>.</returns>
    IChainAsync<TParameter, TResult> Add<TMiddleware>()
        where TMiddleware : IChainMiddlewareAsync<TParameter, TResult>;

    /// <summary>
    /// Chains a new middleware type to the chain of responsibility.
    /// Middleware will be executed in the same order they are added.
    /// </summary>
    /// <param name="middlewareType">The middleware type to be executed.</param>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="middlewareType"/> is 
    /// not an implementation of <see cref="IChainMiddlewareAsync{TParameter, TResult}"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="middlewareType"/> is null.</exception>
    /// <returns>The current instance of <see cref="IChainAsync{TParameter, TResult}"/>.</returns>
    IChainAsync<TParameter, TResult> Add(Type middlewareType);

    /// <summary>
    /// Executes the configured chain of responsibility.
    /// </summary>
    /// <param name="parameter"></param>
    Task<TResult> ExecuteAsync(TParameter parameter);
}
