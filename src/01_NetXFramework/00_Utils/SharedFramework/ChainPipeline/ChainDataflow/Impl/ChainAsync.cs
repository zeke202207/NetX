namespace NetX.SharedFramework.ChainPipeline.ChainDataflow;

/// <summary>
/// 异步责任链链条
/// </summary>
/// <typeparam name="TParameter">The input type for the chain.</typeparam>
/// <typeparam name="TResult">The return type of the chain.</typeparam>
public class ChainAsync<TParameter, TResult> : ChainBase<TParameter, TResult>, IChainAsync<TParameter, TResult>
    where TParameter : DataflowParameter
    where TResult : DataflowResult
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="middlewareCreater"></param>
    public ChainAsync(IMiddlewareCreater middlewareCreater)
        : base(middlewareCreater)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns></returns>
    public IChainAsync<TParameter, TResult> Add<TMiddleware>()
        where TMiddleware : IChainMiddlewareAsync<TParameter, TResult>
    {
        base._middlewareTypes.Insert(0,typeof(TMiddleware));
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="middlewareType"></param>
    /// <returns></returns>
    public IChainAsync<TParameter, TResult> Add(Type middlewareType)
    {
        base._middlewareTypes.Insert(0,middlewareType);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public async Task<TResult> ExecuteAsync(TParameter parameter)
    {
        if (base._middlewareTypes.Count == 0)
            return default(TResult);
        int index = 0;
        Func<TParameter, Task<TResult>> func = null;
        func = async (param) =>
        {
            var type = base._middlewareTypes[index++];
            var middleware = base._middlewareCreater.Create(type) as IChainMiddlewareAsync<TParameter, TResult>;
            if (index == base._middlewareTypes.Count)
                func = async p => await Task.FromResult(base.DefaultResult());
            return await middleware.RunAsync(parameter, func);
        };
        return await func(parameter);
    }
}
