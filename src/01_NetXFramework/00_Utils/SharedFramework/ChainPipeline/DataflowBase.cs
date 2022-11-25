using System.Reflection;

namespace NetX.SharedFramework.ChainPipeline;

/// <summary>
/// 数据流基类
/// </summary>
/// <typeparam name="TMiddleware">中间件</typeparam>
public abstract class DataflowBase<TMiddleware>
{
    /// <summary>
    /// 中间件集合
    /// </summary>
    protected readonly IList<Type> _middlewareTypes;

    /// <summary>
    /// 数据流基类
    /// </summary>
    internal DataflowBase()
    {
        _middlewareTypes = new List<Type>();
    }

    /// <summary>
    /// 添加中间件
    /// </summary>
    /// <param name="middlewareType">中间件类型</param>
    protected void AddMiddleware(Type middlewareType)
    {
        if (null == middlewareType)
            throw new ArgumentNullException();
        if (!typeof(TMiddleware).GetTypeInfo().IsAssignableFrom(middlewareType.GetTypeInfo()))
            throw new ArgumentException($"The middleware type must implement `{typeof(TMiddleware)}`.");
        this._middlewareTypes.Add(middlewareType);
    }
}