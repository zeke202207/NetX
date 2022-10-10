using NetX.Common;
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
    public abstract class LoggingBaseService : BaseService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        protected string SerializeObject<T>(T data)
        {
            if (null == data)
                return string.Empty;
            return Newtonsoft.Json.JsonConvert.SerializeObject(data);
        }
    }
}
