using NetX.Common.ModuleInfrastructure;
using NetX.Ddd.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetX.Common.Attributes;
using NetX.Cache.Core;
using NetX.Caching.Models;

namespace NetX.Caching.Domain
{
    [Scoped]
    public class CachingValueQueryHandler : DomainQueryHandler<CachingValueQuery, ResultModel>
    {
        private readonly Func<string, ICacheProvider> _cacheProviderFactory;

        public CachingValueQueryHandler(IDatabaseContext dbContext, Func<string, ICacheProvider> cacheProviderFactory)
            : base(dbContext)
        {
            _cacheProviderFactory = cacheProviderFactory;
        }

        public override async Task<ResultModel> Handle(CachingValueQuery request, CancellationToken cancellationToken)
        {
            var result = new CachingValueModel() { CacheKey = request.CacheKey, CacheType = request.CacheType };
            var provider = _cacheProviderFactory(request.CacheType);
            if (provider == null || !(await provider.ExistsAsync(request.CacheKey)))
                return result.ToSuccessResultModel();
            var objValue = provider.Get(request.CacheKey);            
            result.CacheValue = Newtonsoft.Json.JsonConvert.SerializeObject(objValue);
            return result.ToSuccessResultModel();
        }
    }
}
