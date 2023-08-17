using NetX.Common.ModuleInfrastructure;
using NetX.Ddd.Domain;

namespace NetX.Caching.Domain
{
    public class CachingKeyQuery : DomainQuery<ResultModel>
    {
        /// <summary>
        /// 缓存类型的唯一标识
        /// </summary>
        public string CacheType { get; set; }

        public CachingKeyQuery(string cacheType)
        {
            CacheType = cacheType;
        }
    }
}
