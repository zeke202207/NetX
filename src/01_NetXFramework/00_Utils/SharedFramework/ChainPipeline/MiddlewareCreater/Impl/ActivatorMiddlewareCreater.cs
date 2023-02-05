namespace NetX.SharedFramework.ChainPipeline;

/// <summary>
/// 反射创建器
/// </summary>
public class ActivatorMiddlewareCreater : IMiddlewareCreater
{
    /// <summary>
    /// 创建中间件
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public object Create(Type type)
    {
        //TODO:Cache   
        return Activator.CreateInstance(type);
    }
}
