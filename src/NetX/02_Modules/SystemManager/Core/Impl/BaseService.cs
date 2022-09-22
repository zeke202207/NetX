namespace NetX.SystemManager.Core;

/// <summary>
/// 服务基类
/// </summary>
public abstract class BaseService
{
    /// <summary>
    /// 统一id生成器
    /// </summary>
    /// <returns></returns>
    protected string CreateId()
    {
        return Guid.NewGuid().ToString("N");
    }

    /// <summary>
    /// 统一时间生成器
    /// </summary>
    /// <returns></returns>
    protected DateTime CreateInsertTime()
    {
        return DateTime.Now;
    }
}
