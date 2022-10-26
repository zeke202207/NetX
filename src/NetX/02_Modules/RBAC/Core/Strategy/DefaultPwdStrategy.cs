using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Core;

/// <summary>
/// 默认密码测率
/// </summary>
public class DefaultPwdStrategy : IPasswordStrategy
{
    /// <summary>
    /// 默认密码测率实例
    /// </summary>
    public DefaultPwdStrategy()
    {
    }

    /// <summary>
    /// 生成密码
    /// </summary>
    /// <returns></returns>
    public Task<string> GeneratePassword()
    {
        return Task.FromResult(RBACConst.C_RBAC_DEFAULT_PASSWORD);
    }
}
