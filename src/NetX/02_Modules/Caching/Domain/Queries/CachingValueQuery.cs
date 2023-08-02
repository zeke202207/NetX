using NetX.Common.ModuleInfrastructure;
using NetX.Ddd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Caching.Domain
{
    public class CachingValueQuery : DomainQuery<ResultModel>
    {
        /// <summary>
        /// 缓存类型的唯一标识
        /// </summary>
        public string CacheType { get; set; }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string CacheKey { get; set; }

        public CachingValueQuery(string cacheType, string cacheKey)
        {
            CacheType = cacheType;
            CacheKey = cacheKey;
        }
    }
}
