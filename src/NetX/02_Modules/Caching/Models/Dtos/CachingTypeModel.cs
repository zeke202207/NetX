using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Caching.Models
{
    public record CachingTypeModel
    {
        public string CacheType { get; set; }

        public string CacheTypeName { get; set; }
    }
}
