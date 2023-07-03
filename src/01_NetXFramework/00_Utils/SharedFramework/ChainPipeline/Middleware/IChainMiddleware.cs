namespace NetX.SharedFramework.ChainPipeline;

/// <summary>
/// 责任链中间件
/// </summary>
/// <typeparam name="TParameter">中间件参数</typeparam>
/// <typeparam name="TResult">中间件返回值</typeparam>
public interface IChainMiddleware<TParameter, TResult>
    where TResult : new()
{
    /// <summary>
    /// 执行中间件
    /// </summary>
    /// <param name="context">中间件传递参数</param>
    /// <param name="next">下一个中间件</param>
    /// <returns></returns>
    ReponseContext<TResult> Run(RequestContext<TParameter> context, Func<RequestContext<TParameter>, ReponseContext<TResult>> next);
}
