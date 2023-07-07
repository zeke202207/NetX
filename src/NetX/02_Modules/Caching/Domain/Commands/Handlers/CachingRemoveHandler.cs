using Netx.Ddd.Domain;
using NetX.Cache.Core;
using NetX.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetX.Caching.Domain
{
    [Scoped]
    public class CachingRemoveHandler : DomainCommandHandler<CachingRemoveByPreKeyCommand>
    {
        private readonly Func<string, ICacheProvider> _cacheProviderFactory;

        public CachingRemoveHandler(Func<string, ICacheProvider> cacheProviderFactory)
        {
            _cacheProviderFactory = cacheProviderFactory;
        }

        public override async Task<bool> Handle(CachingRemoveByPreKeyCommand request, CancellationToken cancellationToken)
        {
            var provider = _cacheProviderFactory(request.CacheType);
            if (provider == null)
                return true;
            await provider.RemoveByPrefixAsync(request.CachePreKey);
            return true;
        }
    }
}
