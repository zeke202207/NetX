namespace NetX.SharedFramework.ChainPipeline.PipelineDataflow;

/// <summary>
/// 异步管道
/// </summary>
/// <typeparam name="TParameter"></typeparam>
public class PipelineAsync<TParameter> : PipelineBase<TParameter>, IPipelineAsync<TParameter>
where TParameter : DataflowParameter
{
    /// <summary>
    /// 异步管道实例
    /// </summary>
    public PipelineAsync(IMiddlewareCreater middlewareCreater)
        : base(middlewareCreater)
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns></returns>
    public IPipelineAsync<TParameter> Add<TMiddleware>() where TMiddleware : IPilelineMiddlewareAsync<TParameter>
    {
        base._middlewareTypes.Add(typeof(TMiddleware));
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="middlewareType"></param>
    /// <returns></returns>
    public IPipelineAsync<TParameter> Add(Type middlewareType)
    {
        base._middlewareTypes.Add(middlewareType);
        return this;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public async Task ExecuteAsync(TParameter parameter)
    {
        if (base._middlewareTypes.Count == 0)
            return;
        int index = 0;
        Func<TParameter, Task> func = null;
        func = async (param) =>
        {
            var type = base._middlewareTypes[index++];
            var middleware = _middlewareCreater.Create(type) as IPilelineMiddlewareAsync<TParameter>;
            if (index == base._middlewareTypes.Count)
                func = async p => await Task.CompletedTask;
            if (null != middleware)
                await middleware.RunAsync(parameter, func);
        };
        await func(parameter);
    }
}
