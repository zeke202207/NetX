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
    public abstract class BaseService
    {
        /// <summary>
        /// 统一id生成器
        /// </summary>
        /// <returns></returns>
        protected string CreateId()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 统一时间生成器
        /// </summary>
        /// <returns></returns>
        protected DateTime CreateInsertTime()
        {
            return DateTime.Now;
        }

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
