using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Caching.Models
{
    public class CachingValueModel
    {
        public string CacheType { get; set; }
        public string CacheKey { get; set; }
        public string CacheValue { get; set; }
    }
}
