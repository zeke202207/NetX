using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.Models;

/// <summary>
/// 系统登录日志实体对象
/// </summary>
public class LoginLoggingModel
{
    /// <summary>
    /// 登录日志唯一表示
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// 登录用户唯一标识
    /// </summary>
    [JsonProperty("userid")]
    public string UserId { get; set; }

    /// <summary>
    /// 登录用户昵称
    /// </summary>
    [JsonProperty("username")]
    public string UserName { get; set; }

    /// <summary>
    /// 登录ip
    /// </summary>
    [JsonProperty("loginip")]
    public string LoginIp { get; set; }

    /// <summary>
    /// 登录位置
    /// </summary>
    [JsonProperty("loginaddress")]
    public string LoginAddress { get; set; }

    /// <summary>
    /// 登录时间
    /// </summary>
    [JsonProperty("createtime")]
    public DateTime CreateTime { get; set; }
}
