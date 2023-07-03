namespace NetX.SharedFramework.ChainPipeline;

/// <summary>
/// 异步管道中间件
/// </summary>
public interface IPilelineMiddlewareAsync<TParameter>
{
    /// <summary>
    /// 执行异步中间件
    /// </summary>
    /// <param name="context">中间件传递参数</param>
    /// <param name="next">下一个中间件</param>
    Task RunAsync(RequestContext<TParameter> context, Func<RequestContext<TParameter>, Task>? next);
}
