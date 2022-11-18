using NetX.Common.ModuleInfrastructure;
using NetX.Tools.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Tools.Core
{
    /// <summary>
    /// 登录日志服务
    /// </summary>
    public interface ILoginLoggingService
    {
        /// <summary>
        /// 获取系统登录日志列表
        /// </summary>
        /// <param name="queryParam">查询参数</param>
        /// <returns></returns>
        Task<ResultModel<PagerResultModel<List<LoginLoggingModel>>>> GetLogininLoggingList(LoginLoggingParam queryParam);
    }
}
