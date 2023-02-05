using Newtonsoft.Json;

namespace NetX.Common.ModuleInfrastructure;

public abstract class ResultModel
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
    /// 异常消息内容
    /// </summary>
    [JsonProperty("message")]
    public string Message { get; set; }
}

