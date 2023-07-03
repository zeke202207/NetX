namespace NetX.SharedFramework.ChainPipeline.PipelineDataflow;

/// <summary>
/// 存储中间件的管道 <see cref="Execute(TParameter)"/> .
/// 中间件的执行顺序和添加顺序保持一致
/// </summary>
/// <typeparam name="TParameter"></typeparam>
public interface IPipelineAsync<TParameter>
{
    /// <summary>
    /// 添加一个需要被执行的中间件
    /// </summary>
    /// <typeparam name="TMiddleware"></typeparam>
    /// <returns></returns>
    IPipelineAsync<TParameter> Add<TMiddleware>()
        where TMiddleware : IPilelineMiddlewareAsync<TParameter>;

    /// <summary>
    /// 添加一个需要被执行的中间件
    /// </summary>
    /// <param name="middlewareType">The middleware type to be executed.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentException">Thrown if the <paramref name="middlewareType"/> is 
    /// not an implementation of <see cref="IPipelineMiddleware{TParameter}"/>.</exception>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="middlewareType"/> is null.</exception>
    IPipelineAsync<TParameter> Add(Type middlewareType);

    /// <summary>
    /// 执行配置好的中间件
    /// </summary>
    /// <param name="parameter"></param>
    Task ExecuteAsync(TParameter parameter);
}
