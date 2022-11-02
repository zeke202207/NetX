using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Models;

/// <summary>
/// 修改免密实体对象
/// </summary>
public class ChangePwdRequestModel
{
    /// <summary>
    /// 旧密码
    /// </summary>
    [JsonProperty("oldpwd")]
    public string OldPassword { get; set; }

    /// <summary>
    /// 新密码
    /// </summary>
    [JsonProperty("newpwd")]
    public string NewPassword { get; set; }
}
