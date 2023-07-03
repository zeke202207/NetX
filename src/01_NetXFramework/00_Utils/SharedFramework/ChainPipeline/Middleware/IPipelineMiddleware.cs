namespace NetX.SharedFramework.ChainPipeline;

/// <summary>
/// 同步管道中间件
/// </summary>
/// <typeparam name="TParameter"></typeparam>
public interface IPipelineMiddleware<TParameter>
{
    /// <summary>
    /// 执行中间件
    /// </summary>
    /// <param name="context">中间件传递参数</param>
    /// <param name="next">下一个中间件</param>
    void Run(RequestContext<TParameter> context, Action<RequestContext<TParameter>>? next);
}
