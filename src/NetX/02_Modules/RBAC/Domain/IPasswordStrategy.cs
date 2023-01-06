using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.RBAC.Domain
{
    /// <summary>
    /// 密码生成策略
    /// 注册用户时，提供密码生成策略
    /// 默认策略：给定默认密码
    /// 可实现 随机密码策略，邮件通知 等等其他策略
    /// </summary>
    public interface IPasswordStrategy
    {
        /// <summary>
        /// 生成密码
        /// </summary>
        /// <returns></returns>
        Task<string> GeneratePassword();
    }
}
