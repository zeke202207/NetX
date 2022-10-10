using NetX.Common.Models;
using NetX.Tools.Models;

namespace NetX.Tools.Core
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
