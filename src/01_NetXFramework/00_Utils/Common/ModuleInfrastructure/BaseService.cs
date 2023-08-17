namespace NetX.Common.ModuleInfrastructure;

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


    /// <summary>
    /// 返回成功结果
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="result"></param>
    /// <returns></returns>
    protected ResultModel<T> Success<T>(T result)
    {
        return new ResultModel<T>(ResultEnum.SUCCESS) { Result = result };
    }

    /// <summary>
    /// 返回失败结果
    /// </summary>
    /// <param name="resultEnum"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    protected ResultModel<T> Error<T>(string msg)
    {
        return new ResultModel<T>(ResultEnum.ERROR) { Message = msg };
    }

    /// <summary>
    /// 返回失败结果
    /// </summary>
    /// <param name="resultEnum"></param>
    /// <param name="msg"></param>
    /// <returns></returns>
    protected ResultModel<T> Error<T>(ResultEnum resultEnum, string msg)
    {
        return new ResultModel<T>(resultEnum) { Message = msg };
    }
}
