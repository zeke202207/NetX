using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Authentication;

/// <summary>
///  登录处理接口
/// </summary>
public interface ILoginHandler
{
    /// <summary>
    /// 登录处理
    /// </summary>
    /// <param name="claimModel">信息</param>
    /// <param name="extendData">扩展数据</param>
    /// <returns></returns>
    dynamic Handle(ClaimModel claimModel, string extendData);
}
