using NetX.Common;

namespace NetX.Tools.Core
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
