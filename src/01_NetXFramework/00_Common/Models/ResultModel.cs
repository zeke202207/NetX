using Newtonsoft.Json;

namespace NetX.Common.Models;

/// <summary>
/// 统一结果实体对象
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResultModel<T>
{
    /// <summary>
    /// 统一返回结果实体对象
    /// </summary>
    /// <param name="code"></param>
    public ResultModel(ResultEnum code)
    {
        this.Code = code;
    }

    /// <summary>
    /// 服务器处理结果状态
    /// </summary>
    [JsonProperty("code")]
    public ResultEnum Code { get; private set; }

    /// <summary>
    /// 服务器处理结果对象
    /// </summary>
    [JsonProperty("result")]
    public T? Result { get; set; }

    /// <summary>
    /// 异常消息内容
    /// </summary>
    [JsonProperty("message")]
    public string? Message { get; set; }
}

/// <summary>
/// 结果状态枚举
/// </summary>
public enum ResultEnum
{
    /// <summary>
    /// 成功
    /// </summary>
    SUCCESS = 0,
    /// <summary>
    /// 失败
    /// </summary>
    ERROR = -1,
    /// <summary>
    /// 超时
    /// </summary>
    TIMEOUT = 401,
}
