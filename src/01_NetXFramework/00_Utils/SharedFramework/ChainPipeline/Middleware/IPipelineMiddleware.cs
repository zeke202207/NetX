namespace NetX.SharedFramework.ChainPipeline;

/// <summary>
/// 同步管道中间件
/// </summary>
/// <typeparam name="TParameter"></typeparam>
public interface IPipelineMiddleware<TParameter>
    where TParameter : DataflowParameter
{
    /// <summary>
    /// 执行中间件
    /// </summary>
    /// <param name="parameter">中间件传递参数</param>
    /// <param name="next">下一个中间件</param>
    void Run(TParameter parameter, Action<TParameter>? next);
}
