namespace NetX.SharedFramework.ChainPipeline.ChainDataflow;

/// <summary>
/// 责任链处理基类
/// </summary>
/// <typeparam name="TParameter"></typeparam>
/// <typeparam name="TResult"></typeparam>
public abstract class ChainBase<TParameter, TResult> : DataflowBase<IChainMiddleware<TParameter, TResult>>
        where TParameter : DataflowParameter
        where TResult : DataflowResult
{
    /// <summary>
    /// 中间件构造器
    /// </summary>
    protected readonly IMiddlewareCreater _middlewareCreater;

    /// <summary>
    /// 管道实例
    /// </summary>
    public ChainBase(IMiddlewareCreater middlewareCreater)
    {
        this._middlewareCreater = middlewareCreater;
    }

    /// <summary>
    /// 默认结果
    /// </summary>
    /// <returns></returns>
    protected virtual TResult DefaultResult()
    {
        return Activator.CreateInstance(typeof(TResult)) as TResult;
    }
}

