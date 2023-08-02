using NetX.Ddd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Caching.Domain
{
    public record CachingRemoveByPreKeyCommand : DomainCommand
    {
        // <summary>
        /// 缓存类型的唯一标识
        /// </summary>
        public string CacheType { get; set; }

        /// <summary>
        /// 缓存key
        /// </summary>
        public string CachePreKey { get; set; }

        public CachingRemoveByPreKeyCommand(string cacheType, string cachePreKey)
        {
            CacheType = cacheType;
            CachePreKey = cachePreKey;
        }
    }
}
