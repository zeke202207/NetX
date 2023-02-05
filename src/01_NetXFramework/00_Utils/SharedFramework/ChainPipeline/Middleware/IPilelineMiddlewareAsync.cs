namespace NetX.SharedFramework.ChainPipeline;

/// <summary>
/// 异步管道中间件
/// </summary>
public interface IPilelineMiddlewareAsync<TParameter>
        where TParameter : DataflowParameter
{
    /// <summary>
    /// 执行异步中间件
    /// </summary>
    /// <param name="parameter">中间件传递参数</param>
    /// <param name="next">下一个中间件</param>
    Task RunAsync(TParameter parameter, Func<TParameter, Task>? next);
}
