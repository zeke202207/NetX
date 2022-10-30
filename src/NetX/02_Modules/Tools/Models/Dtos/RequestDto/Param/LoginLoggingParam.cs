using NetX.Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.Models;

/// <summary>
/// 登录日志请求参数
/// </summary>
public class LoginLoggingParam : Pager
{
    /// <summary>
    /// 登录用户昵称
    /// </summary>
    [JsonProperty("username")]
    public string? UserName { get; set; }
}
