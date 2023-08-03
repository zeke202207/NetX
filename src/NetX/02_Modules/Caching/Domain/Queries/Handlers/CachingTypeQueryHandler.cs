using NetX.Ddd.Domain;
using NetX.Cache.Core;
using NetX.Caching.Models;
using NetX.Common.Attributes;
using NetX.Common.ModuleInfrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Caching.Domain
{
    [Scoped]
    public class CachingTypeQueryHandler : DomainQueryHandler<CachingTypeQuery, ResultModel>
    {
        private readonly IEnumerable<ICacheProvider> _cacheProviders;

        public CachingTypeQueryHandler(IDatabaseContext dbContext, IEnumerable<ICacheProvider> cacheProviders) 
            : base(dbContext)
        {
            _cacheProviders = cacheProviders;
        }

        public override async Task<ResultModel> Handle(CachingTypeQuery request, CancellationToken cancellationToken)
        {
            //CachingTypeModel
            var result = _cacheProviders.ToList().Select(provider =>
            {
                var typeKey = provider.GetType().GetProperty("CacheType");
                var cacheName = provider.GetType().GetProperty("CacheName");
                var result = new CachingTypeModel()
                {
                    CacheType = typeKey.GetValue(provider)?.ToString(),
                    CacheTypeName = cacheName.GetValue(provider)?.ToString()
                };
                return result;
            });
            return result.ToSuccessResultModel();
        }
    }
}
