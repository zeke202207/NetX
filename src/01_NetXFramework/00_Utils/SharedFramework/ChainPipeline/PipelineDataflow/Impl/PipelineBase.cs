namespace NetX.SharedFramework.ChainPipeline.PipelineDataflow;

/// <summary>
/// 管道处理基类
/// </summary>
/// <typeparam name="TParameter"></typeparam>
public abstract class PipelineBase<TParameter> : DataflowBase<IPipelineMiddleware<TParameter>>
    where TParameter : DataflowParameter
{
    /// <summary>
    /// 中间件构造器
    /// </summary>
    protected readonly IMiddlewareCreater _middlewareCreater;

    /// <summary>
    /// 管道实例
    /// </summary>
    public PipelineBase(IMiddlewareCreater middlewareCreater)
    {
        this._middlewareCreater = middlewareCreater;
    }
}
