using NetX.Common.Models;
using NetX.LogCollector.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.LogCollector.Core
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILoggingCollectorService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="loggingParam"></param>
        /// <returns></returns>
        Task<ResultModel<PagerResultModel<List<LoggingModel>>>> GetLoggingList(LoggingParam loggingParam);
    }
}
