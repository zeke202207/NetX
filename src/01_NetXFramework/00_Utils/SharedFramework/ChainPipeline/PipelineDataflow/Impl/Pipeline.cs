namespace NetX.SharedFramework.ChainPipeline.PipelineDataflow;

/// <summary>
/// 同步管道
/// </summary>
/// <typeparam name="TParameter"></typeparam>
public class Pipeline<TParameter> : PipelineBase<TParameter>, IPipeline<TParameter>
{
    /// <summary>
    /// 同步管道实例
    /// </summary>
    public Pipeline(IMiddlewareCreater middlewareCreater)
        : base(middlewareCreater)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns></returns>
    public virtual IPipeline<TParameter> Add<TMiddleware>()
        where TMiddleware : IPipelineMiddleware<TParameter>
    {
        base._middlewareTypes.Add(typeof(TMiddleware));
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="middlewareType"></param>
    /// <returns></returns>
    public virtual IPipeline<TParameter> Add(Type middlewareType)
    {
        base._middlewareTypes.Add(middlewareType);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    public void Execute(TParameter parameter)
    {
        try
        {
            if (base._middlewareTypes.Count == 0)
                return;
            int index = 0;
            Action<RequestContext<TParameter>> action = null;
            action = parameter =>
            {
                try
                {
                    var type = base._middlewareTypes[index++];
                    var middleware = _middlewareCreater.Create(type) as IPipelineMiddleware<TParameter>;
                    if (index == base._middlewareTypes.Count)
                        action = p => { };
                    if (parameter.CancellationToken.IsCancellationRequested)
                        throw new Exception("pipeline is canneled");
                    if (null != middleware)
                        middleware.Run(parameter, action);
                }
                catch (Exception ex)
                {
                    throw new ChainPipelineExcpetion(ex);
                }
            };
            action(new RequestContext<TParameter>(parameter));
        }
        catch (Exception ex)
        {
            throw new ChainPipelineExcpetion(ex);
        }
    }
}
