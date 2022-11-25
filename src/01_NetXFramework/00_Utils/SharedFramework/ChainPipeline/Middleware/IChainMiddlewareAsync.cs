namespace NetX.SharedFramework.ChainPipeline;

/// <summary>
/// 异步责任链中间件
/// </summary>
/// <typeparam name="TParameter">中间件参数</typeparam>
/// <typeparam name="TResult">中间件返回值</typeparam>
public interface IChainMiddlewareAsync<TParameter, TResult>
    where TParameter : DataflowParameter
    where TResult : DataflowResult
{
    /// <summary>
    /// 执行异步中间件
    /// </summary>
    /// <param name="parameter">中间件传递参数</param>
    /// <param name="next">下一个中间件</param>
    /// <returns></returns>
    Task<TResult> RunAsync(TParameter parameter, Func<TParameter, Task<TResult>> next);
}
