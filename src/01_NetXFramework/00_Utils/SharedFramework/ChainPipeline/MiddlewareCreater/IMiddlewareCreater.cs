namespace NetX.SharedFramework.ChainPipeline;

/// <summary>
/// 创建一个中间件实例
/// </summary>
public interface IMiddlewareCreater
{
    /// <summary>
    /// 创建中间件实例类型
    /// </summary>
    /// <param name="type">需要创建的中间件类型</param>
    /// <returns>中间件实例</returns>
    object Create(Type type);
}
