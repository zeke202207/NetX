namespace NetX.SharedFramework.ChainPipeline.ChainDataflow;

/// <summary>
/// 异步责任链链条
/// </summary>
/// <typeparam name="TParameter">The input type for the chain.</typeparam>
/// <typeparam name="TResult">The return type of the chain.</typeparam>
public class ChainAsync<TParameter, TResult> : ChainBase<TParameter, TResult>, IChainAsync<TParameter, TResult>
    where TResult : class, new()
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
        base._middlewareTypes.Insert(0, typeof(TMiddleware));
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="middlewareType"></param>
    /// <returns></returns>
    public IChainAsync<TParameter, TResult> Add(Type middlewareType)
    {
        base._middlewareTypes.Insert(0, middlewareType);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public async Task<ReponseContext<TResult>> ExecuteAsync(TParameter parameter)
    {
        ReponseContext<TResult> result = new ReponseContext<TResult>();
        try
        {
            if (base._middlewareTypes.Count == 0)
                return result;
            int index = 0;
            Func<RequestContext<TParameter>, Task<ReponseContext<TResult>>> func = null;
            func = async (param) =>
            {
                try
                {
                    var type = base._middlewareTypes[index++];
                    var middleware = base._middlewareCreater.Create(type) as IChainMiddlewareAsync<TParameter, TResult>;
                    if (index == base._middlewareTypes.Count)
                        func = async p => await Task.FromResult(base.DefaultResult());
                    if (null != middleware)
                        return await middleware.RunAsync(param, func);
                    else
                        throw new Exception("the middleware is null");
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Ex.Add(new ChainPipelineExcpetion(ex));
                    return result;
                }
            };
            return await func(new RequestContext<TParameter>(parameter));
        }
        catch (Exception ex)
        {
            result.Success = false;
            result.Ex.Add(new ChainPipelineExcpetion(ex));
            return result;
        }
    }
}
