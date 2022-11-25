namespace NetX.SharedFramework.ChainPipeline.ChainDataflow;

/// <summary>
/// 同步责任链链条
/// </summary>
/// <typeparam name="TParameter">The input type for the chain.</typeparam>
/// <typeparam name="TResult">The return type of the chain.</typeparam>
public class Chain<TParameter, TResult> : ChainBase<TParameter, TResult>, IChain<TParameter, TResult>
    where TParameter : DataflowParameter
    where TResult : DataflowResult
{
    /// <summary>
    /// 同步责任链链条实例
    /// </summary>
    /// <param name="middlewareCreater"></param>
    public Chain(IMiddlewareCreater middlewareCreater)
        : base(middlewareCreater)
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns></returns>
    public IChain<TParameter, TResult> Add<TMiddleware>()
        where TMiddleware : IChainMiddleware<TParameter, TResult>
    {
        base._middlewareTypes.Insert(0, typeof(TMiddleware));
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="middlewareType"></param>
    /// <returns></returns>
    public IChain<TParameter, TResult> Add(Type middlewareType)
    {
        base._middlewareTypes.Insert(0, middlewareType);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public TResult Execute(TParameter parameter)
    {
        if (base._middlewareTypes.Count == 0)
            return default(TResult);
        int index = 0;
        Func<TParameter, TResult> func = null;
        func = (param) =>
        {
            var type = base._middlewareTypes[index++];
            var middleware = base._middlewareCreater.Create(type) as IChainMiddleware<TParameter, TResult>;
            if (index == base._middlewareTypes.Count)
                func = p => base.DefaultResult();
            return middleware.Run(parameter, func);
        };
        return func(parameter);
    }
}
