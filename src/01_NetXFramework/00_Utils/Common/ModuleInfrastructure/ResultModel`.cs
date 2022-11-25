using NetX.Common;
using NetX.Common.ModuleInfrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Common.ModuleInfrastructure;

/// <summary>
/// 统一结果实体对象
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResultModel<T> : ResultModel
{
    /// <summary>
    /// 统一返回结果实体对象
    /// </summary>
    /// <param name="code"></param>
    public ResultModel(ResultEnum code)
        : base(code)
    {

    }

    /// <summary>
    /// 服务器处理结果对象
    /// </summary>
    [JsonProperty("result")]
    public T Result { get; set; }
}
